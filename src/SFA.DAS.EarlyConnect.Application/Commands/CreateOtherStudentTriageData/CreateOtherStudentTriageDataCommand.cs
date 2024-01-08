using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData
{
    public class CreateOtherStudentTriageDataCommand : IRequest<CreateOtherStudentTriageDataCommandResponse>
    {
        public string Email { get; set; }
        public string LepsCode { get; set; }
    }
}
