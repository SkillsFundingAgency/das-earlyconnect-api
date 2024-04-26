using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData
{
    public class SendReminderEmailCommandHandler : IRequestHandler<SendReminderEmailCommand, SendReminderEmailResult>
    {
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly ILogger<SendReminderEmailCommandHandler> _logger;
        private readonly IMessageSession _messageSession;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly EarlyConnectApiConfiguration _earlyConnectApiConfiguration;
        public const string TemplateId = "EarlyConnectSurveyReminderEmail";

        public SendReminderEmailCommandHandler(
            IStudentDataRepository studentDataRepository,
            ILogger<SendReminderEmailCommandHandler> logger,
            IMessageSession messageSession,
            IStudentSurveyRepository studentSurveyRepository,
            EarlyConnectApiConfiguration earlyConnectApiConfiguration)
        {
            _studentDataRepository = studentDataRepository;
            _logger = logger;
            _messageSession = messageSession;
            _studentSurveyRepository = studentSurveyRepository;
            _earlyConnectApiConfiguration = earlyConnectApiConfiguration;
        }

        public async Task<SendReminderEmailResult> Handle(SendReminderEmailCommand command, CancellationToken cancellationToken)
        {
            var students = await _studentDataRepository.GetEmailByLepcodeAsync(command.LepsCode, "Other");

            if (!students.Any())
                return new SendReminderEmailResult { Message = "No records found" };

            foreach (var studentData in students)
            {
                var tokens = new Dictionary<string, string>
                                {
                                    { "Contact", $"{studentData.FirstName}" },
                                    { "RemainderURL", $"{_earlyConnectApiConfiguration.BaseUrl}{studentData.LEPSData.LepCode}" },
                                };

                await _messageSession.Send(new SendEmailCommand(TemplateId, studentData.Email, tokens));
                await _studentSurveyRepository.UpdateStudentSurveyReminderEmailDateAsync(studentData.StudentSurveys.FirstOrDefault()?.Id);
            }

            return new SendReminderEmailResult { Message = $"Sending remainder emails for {students.Count} students completed" };
        }
    }
}
