using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData
{
    public class CreateMetricsDataCommand
    {
        public ICollection<ApprenticeMetricsData> MetricsData { get; set; }
    }
}
