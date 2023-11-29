using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IMetricsDataRepository
    {
        Task AddManyAndDelete(IEnumerable<ApprenticeMetricsData> metricsData);
        Task<ICollection<ApprenticeMetricsData>> GetByLepsIdAsync(int lepsId);
    }
}
