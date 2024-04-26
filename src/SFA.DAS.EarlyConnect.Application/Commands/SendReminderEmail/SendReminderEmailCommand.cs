using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData
{
    public class SendReminderEmailCommand : IRequest<SendReminderEmailResult>
    {
        public string LepsCode { get; set; }
    }
}