using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class MetricsFlagRepository : IMetricsFlagRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public MetricsFlagRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MetricsFlag>> GetMetricsFlagAsync()
        {
            return await _dbContext.MetricsFlag
                .Where(a => a.IsActive == true)
                .Select(x => new MetricsFlag
                {
                    Id = x.Id,
                    FlagName = x.FlagName,
                    FlagCode = x.FlagCode
                })
                .ToListAsync();
        }

    }
}
