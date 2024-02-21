using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentOnboardData
{
    public class CreateStudentOnboardDataCommandHandler : IRequestHandler<CreateStudentOnboardDataCommand, CreateStudentOnboardDataCommandResponse>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly IDataProtectorService _dataProtectorService;
        private readonly IMessageSession _messageSession;
        private readonly ILogger<CreateStudentOnboardDataCommandHandler> _logger;
        private readonly EarlyConnectApiConfiguration _earlyConnectApiConfiguration;
        public const string TemplateId = "EarlyConnectOnboardEmail";

        public CreateStudentOnboardDataCommandHandler(
            ISurveyRepository surveyRepository,
            IStudentDataRepository studentDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            IDataProtectorService dataProtectorService,
            IMessageSession messageSession,
            ILogger<CreateStudentOnboardDataCommandHandler> logger,
            EarlyConnectApiConfiguration earlyConnectApiConfiguration)
        {
            _surveyRepository = surveyRepository;
            _studentDataRepository = studentDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _dataProtectorService = dataProtectorService;
            _messageSession = messageSession;
            _logger = logger;
            _earlyConnectApiConfiguration = earlyConnectApiConfiguration;
        }

        public async Task<CreateStudentOnboardDataCommandResponse> Handle(CreateStudentOnboardDataCommand command, CancellationToken cancellationToken)
        {
            CreateStudentOnboardDataCommandResponse createStudentOnboardDataCommandResponse = new CreateStudentOnboardDataCommandResponse();
            var survey = await _surveyRepository.GetDefaultSurveyAsync();

            foreach (var email in command.Emails)
            {
                var students = await _studentDataRepository.GetByEmailAsync(email, "UCAS");

                if (students?.Any() != true)
                {
                    createStudentOnboardDataCommandResponse.Message = createStudentOnboardDataCommandResponse.Message?.Length > 0
                        ? $"{createStudentOnboardDataCommandResponse.Message}, {email}"
                        : $"Unable to find {email}";
                }
                else
                {
                    foreach (var student in students)
                    {
                        if (student.LepsId.HasValue && student.LepsId > 0)
                        {
                            var existingSurvey =
                            await _studentSurveyRepository.GetStudentSurveyByStudentIdAsync(student.Id, survey.Id);

                            if (existingSurvey == null || existingSurvey.DateEmailSent == null)
                            {
                                if (existingSurvey == null)
                                {
                                    var studentSurveyId = await _studentSurveyRepository.AddStudentSurveyAsync(
                                        new StudentSurvey
                                        {
                                            StudentId = student.Id,
                                            SurveyId = survey.Id
                                        });
                                }

                                existingSurvey =
                                    await _studentSurveyRepository.GetStudentSurveyByStudentIdAsync(student.Id,
                                        survey.Id);

                                _logger.LogInformation(
                                    $"Student Survey created for student with student survey id {existingSurvey.Id}");

                                var encryptedCode =
                                    _dataProtectorService.EncodedData(
                                        $"{existingSurvey.Id} | {DateTime.Now}".ToString());

                                var tokens = new Dictionary<string, string>
                                {
                                    { "Contact", $"{student.FirstName} {student.LastName}" },
                                    { "OnboardURL", $"{_earlyConnectApiConfiguration.BaseUrl}ref?{encryptedCode}" },
                                };

                                _logger.LogInformation($"Sending Email to Student to onboard");

                                await _messageSession.Send(new SendEmailCommand(TemplateId, email, tokens));

                                existingSurvey.DateEmailSent = DateTime.Now;

                                await _studentSurveyRepository.UpdateStudentSurveyAsync(existingSurvey);
                            }
                            else
                            {
                                createStudentOnboardDataCommandResponse.Message = createStudentOnboardDataCommandResponse.Message?.Length > 0
                                    ? $"{createStudentOnboardDataCommandResponse.Message}, {email}"
                                    : $"The email has already been sent{email}";
                            }
                        }
                        else
                        {
                            createStudentOnboardDataCommandResponse.Message = createStudentOnboardDataCommandResponse.Message?.Length > 0
                                ? $"{createStudentOnboardDataCommandResponse.Message}, {email}"
                                : $"Matching LEPS code could not be found to send an onboard {email}";
                        }
                    }
                }
            }

            return createStudentOnboardDataCommandResponse;
        }

    }
}
