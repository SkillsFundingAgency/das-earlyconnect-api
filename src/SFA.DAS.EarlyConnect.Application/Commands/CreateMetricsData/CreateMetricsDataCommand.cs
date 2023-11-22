using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData
{
    public class CreateMetricsDataCommand : IRequest<CreateMetricsDataResponse>
    {
        public ICollection<MetricDto> MetricsData { get; set; }
    }
}
