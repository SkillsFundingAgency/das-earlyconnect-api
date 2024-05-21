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
        private readonly ILEPSDataRepository _lepsDataRepository;
        private readonly EarlyConnectApiConfiguration _earlyConnectApiConfiguration;
        public const string TemplateId = "EarlyConnectSurveyReminderEmail";

        public SendReminderEmailCommandHandler(
            IStudentDataRepository studentDataRepository,
            ILogger<SendReminderEmailCommandHandler> logger,
            IMessageSession messageSession,
            IStudentSurveyRepository studentSurveyRepository,
            ILEPSDataRepository lepsDataRepository,
            EarlyConnectApiConfiguration earlyConnectApiConfiguration)
        {
            _studentDataRepository = studentDataRepository;
            _logger = logger;
            _messageSession = messageSession;
            _studentSurveyRepository = studentSurveyRepository;
            _lepsDataRepository = lepsDataRepository;
            _earlyConnectApiConfiguration = earlyConnectApiConfiguration;
        }

        public async Task<SendReminderEmailResult> Handle(SendReminderEmailCommand command, CancellationToken cancellationToken)
        {
            int counter = 0;

            var students = await _studentDataRepository.GetBySourceAsync("Other");

            if (!students.Any())
                return new SendReminderEmailResult { Message = "No records found" };

            foreach (var studentData in students)
            {
                if (studentData.LepsId != null)
                {
                    var lepsCode = _lepsDataRepository.GetLepsCodeByLepsIdAsync(studentData.LepsId.Value);

                    if (string.IsNullOrEmpty(command.LepsCode) || command.LepsCode.Trim() == lepsCode.Result.Trim())
                    {
                        var tokens = new Dictionary<string, string>
                        {
                            { "Contact", $"{studentData.FirstName}" },
                            { "RemainderURL", $"{_earlyConnectApiConfiguration.BaseUrl}{lepsCode.Result}" },
                        };

                        await _messageSession.Send(new SendEmailCommand(TemplateId, studentData.Email, tokens));
                        await _studentSurveyRepository.UpdateStudentSurveyReminderEmailDateAsync(studentData.StudentSurveys.FirstOrDefault()?.Id);

                        counter++;
                    }
                }
            }

            return new SendReminderEmailResult { Message = $"Reminder email sent to {counter} students" };
        }
    }
}
