using MediatR;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentFeedbackCommand
{
    public class CreateStudentFeedbackCommand : IRequest<CreateStudentFeedbackResult>
    {
        public IEnumerable<StudentFeedback> StudentFeedbackList { get; set; }
    }
}
