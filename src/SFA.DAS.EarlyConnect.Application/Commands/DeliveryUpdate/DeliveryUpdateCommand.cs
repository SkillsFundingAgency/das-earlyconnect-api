using MediatR;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Commands.DeliveryUpdate
{
    public class DeliveryUpdateCommand : IRequest<DeliveryUpdateResult>
    {
        public int LogId { get; set; }
        public string Source { get; set; }
        public List<int> Ids { get; set; }
    }
}
