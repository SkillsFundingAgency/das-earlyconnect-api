using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentOnboardData
{
    public class CreateStudentOnboardDataCommand : IRequest<CreateStudentOnboardDataCommandResponse>
    {
        public IList<string> Emails { get; set; }
    }
}
