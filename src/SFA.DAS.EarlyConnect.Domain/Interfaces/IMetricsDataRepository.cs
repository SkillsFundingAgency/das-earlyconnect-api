using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IMetricsDataRepository
    {
        Task AddManyAsync(IEnumerable<ApprenticeMetricsData> metricsData);
        Task<ICollection<ApprenticeMetricsData>> GetByLepsIdAsync(int lepsId);
    }
}
