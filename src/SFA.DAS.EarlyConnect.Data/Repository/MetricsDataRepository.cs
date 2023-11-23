using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Data.Models;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System.Linq;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class MetricsDataRepository : IMetricsDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public MetricsDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddManyAsync(IEnumerable<ApprenticeMetricsData> metricsData)
        {
            foreach (ApprenticeMetricsData metrics in metricsData)
            {
                metrics.DateAdded = DateTime.Now;
                await _dbContext.AddAsync(metrics);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ApprenticeMetricsData>> GetByLepsIdAsync(int lepsId)
        {
            return await _dbContext.MetricsData
                    .Where(m => m.LEPSId == lepsId && m.IsDeleted == false)
                    .Include(m => m.MetricsFlagLookups)
                    .ToListAsync();
        }
    }
}
