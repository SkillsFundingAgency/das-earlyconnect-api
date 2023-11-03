using MediatR;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateLog
{
    public class CreateLogCommand : IRequest<int>
    {
        public ECAPILog Log { get; set; }
    }
}
