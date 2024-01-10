using Azure.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Application.Services.AuthCodeService;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData
{
    public class CreateOtherStudentTriageDataCommandHandler : IRequestHandler<CreateOtherStudentTriageDataCommand, CreateOtherStudentTriageDataCommandResponse>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly ILEPSDataRepository _lepsDataRepository;    
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly IDataProtectorService _dataProtectorService;
        private readonly IAuthCodeService _authCodeService;
        private readonly IMessageSession _messageSession;
        private readonly ILogger<CreateOtherStudentTriageDataCommandHandler> _logger;
        private readonly IMediator _mediator;
        public const string TemplateId = "EarlyConnectAuthenticationEmail";

        public CreateOtherStudentTriageDataCommandHandler(
            ISurveyRepository surveyRepository,
            IStudentDataRepository studentDataRepository,
            ILEPSDataRepository lepsDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            IDataProtectorService dataProtectorService,
            IAuthCodeService authCodeService,
            IMessageSession messageSession,
            ILogger<CreateOtherStudentTriageDataCommandHandler> logger,
            IMediator mediator)
        {
            _surveyRepository = surveyRepository;
            _studentDataRepository = studentDataRepository;
            _lepsDataRepository = lepsDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _dataProtectorService = dataProtectorService;
            _authCodeService = authCodeService;
            _messageSession = messageSession;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<CreateOtherStudentTriageDataCommandResponse> Handle(CreateOtherStudentTriageDataCommand command, CancellationToken cancellationToken)
        {
            // 0. Fetch the first or default survey record
            var survey = await _surveyRepository.GetDefaultSurveyAsync();

            var lepsId = await _lepsDataRepository.GetLepsIdByLepsCodeAsync(command.LepsCode);

            var logId = await _mediator.Send(new CreateLogCommand
            {
                Log = new ECAPILog 
                {
                    RequestType = "CreateOtherStudentTriageDataCommand",
                    RequestSource = "Other",
                    Status = "Processing",
                    Error = "",
                    FileName = "",
                    Payload = "",
                    RequestIP = ""
                }
            });

            // 1. Create Student Data (dummy)
            var student = await _studentDataRepository.GetByEmailAsync(command.Email, "Other");
            var studentId = (student == null) ? await _studentDataRepository.AddStudentDataAsync(new StudentData
            {
                Email = command.Email,
                LogId = logId,
                LepsId = lepsId,
                FirstName = "",
                LastName = "",
                Industry = "",
                Telephone = "",
                Postcode = "",
                DataSource = "Other"
            }) : student.Id;

            // 2. Create Student Survey
            var studentSurvey = new StudentSurvey
            {
                StudentId = studentId,
                SurveyId = survey.Id
            };

            var studentSurveyId = await _studentSurveyRepository.AddStudentSurveyAsync(studentSurvey);

            _logger.LogInformation($"Student Survey created for student with student survey id {studentSurveyId}");

            // update log status
            await _mediator.Send(new UpdateLogCommand
            {
                LogId = logId,
                Status = "Completed"
            });

            // 3. Generate auth code using the AuthCodeService
            var authCode = _authCodeService.Generate6DigitCode();

            // 4. Encode authCode
            var encryptedAuthCode = _dataProtectorService.EncodedData(authCode);

            // 5. Send Email using Das Notifications Service using authCode
            var tokens = new Dictionary<string, string> {{ "confirmation code", authCode }};

            _logger.LogInformation($"Sending Email to Student to Confirm Email");

            await _messageSession.Send(new SendEmailCommand(TemplateId, command.Email, tokens));

            return new CreateOtherStudentTriageDataCommandResponse
            {
                StudentSurveyId = studentSurveyId.ToString(),
                AuthCode = encryptedAuthCode,
                ExpiryDate = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
