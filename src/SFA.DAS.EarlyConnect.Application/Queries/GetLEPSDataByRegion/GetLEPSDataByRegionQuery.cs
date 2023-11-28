using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByRegion
{
    public class GetLEPSDataByRegionQuery : IRequest<int>
    {
        public string Region { get; set; }
    }
}
