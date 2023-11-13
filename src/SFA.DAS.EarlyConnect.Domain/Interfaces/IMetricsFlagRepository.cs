using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IMetricsFlagRepository
    {
        Task<IEnumerable<MetricsFlag>> GetMetricsFlagAsync();
    }
}
