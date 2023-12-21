using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
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

        public CreateOtherStudentTriageDataCommandHandler(
            ISurveyRepository surveyRepository,
            IStudentDataRepository studentDataRepository,
            ILEPSDataRepository lepsDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            IDataProtectorService dataProtectorService,
            IAuthCodeService authCodeService,
            IMessageSession messageSession,
            ILogger<CreateOtherStudentTriageDataCommandHandler> logger)
        {
            _surveyRepository = surveyRepository;
            _studentDataRepository = studentDataRepository;
            _lepsDataRepository = lepsDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _dataProtectorService = dataProtectorService;
            _authCodeService = authCodeService;
            _messageSession = messageSession;
            _logger = logger;
        }

        public async Task<CreateOtherStudentTriageDataCommandResponse> Handle(CreateOtherStudentTriageDataCommand command, CancellationToken cancellationToken)
        {
            // 0. Fetch the first or default survey record
            var survey = await _surveyRepository.GetDefaultSurveyAsync();

            var lepsId = await _lepsDataRepository.GetLepsIdByLepsCodeAsync(command.LepsCode);

            // 1. Create Student Data (dummy)
            var student = await _studentDataRepository.GetByEmailAsync(command.Email);
            var studentId = (student == null) ? await _studentDataRepository.AddStudentDataAsync(new StudentData
            {
                Email = command.Email,
                LepsId = lepsId
            }) : student.Id;

            // 2. Create Student Survey
            var studentSurvey = new StudentSurvey
            {
                StudentId = studentId,
                SurveyId = survey.Id,
                DateAdded = DateTime.UtcNow
            };

            var studentSurveyId = await _studentSurveyRepository.AddStudentSurveyAsync(studentSurvey);

            _logger.LogInformation($"Student Survey created for student with student survey id {studentSurveyId}");

            // 3. Generate auth code using the AuthCodeService
            var authCode = _authCodeService.Generate6DigitCode();

            // 4. Encode authCode
            var encryptedAuthCode = _dataProtectorService.EncodedData(authCode);

            // 5. Send Email using Das Notifications Service using authCode
            await _messageSession.Send(new SendEmailCommand(
                    "Your GOV Notify email template ID",
                    command.Email,
                    new Dictionary<string, string>() { { "TokenKey", "TokenValue" } }));

            return new CreateOtherStudentTriageDataCommandResponse
            {
                StudentSurveyId = studentSurveyId.ToString(),
                AuthCode = encryptedAuthCode,
                ExpiryDate = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}
