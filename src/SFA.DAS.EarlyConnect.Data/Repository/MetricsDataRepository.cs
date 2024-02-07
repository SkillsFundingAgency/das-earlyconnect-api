using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class MetricsDataRepository : IMetricsDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public MetricsDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddManyAndDelete(IEnumerable<ApprenticeMetricsData> metricsData)
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Step 1: Hard Delete the Soft Deleted Records

                        var metricsDataToDelete = _dbContext.MetricsData.Where(metrics => metrics.IsDeleted);
                        _dbContext.MetricsData.RemoveRange(metricsDataToDelete);

                        await _dbContext.SaveChangesAsync();

                        // Step 2: Soft Delete Existing Records

                        var allMetricsData = _dbContext.MetricsData.Include(data => data.MetricsFlagLookups)
                            .Where(metrics => !metrics.IsDeleted);

                        foreach (var record in allMetricsData)
                        {
                            record.IsDeleted = true;

                            if (record.MetricsFlagLookups != null)
                            {
                                foreach (var flagData in record.MetricsFlagLookups)
                                {
                                    flagData.IsDeleted = true;
                                }
                            }
                        }

                        await _dbContext.SaveChangesAsync();

                        // Step 3: Add New Records

                        foreach (var metrics in metricsData)
                        {
                            metrics.DateAdded = DateTime.Now;
                            await _dbContext.AddAsync(metrics);
                        }

                        await _dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }

        public async Task<ICollection<ApprenticeMetricsData>> GetByLepsIdAsync(int lepsId)
        {
            return await _dbContext.MetricsData
                    .Where(m => m.LEPSId == lepsId && m.IsDeleted == false)
                    .Include(m => m.MetricsFlagLookups)
                    .ToListAsync();
        }

        public async Task<List<int>> UpdateLepsDateSent(IList<int> ids)
        {
            DateTime now = DateTime.Now;
            var invalidIds = new List<int>();

            foreach (var metricId in ids)
            {
                var metric = await _dbContext.MetricsData.Where(student => student.Id == metricId).FirstOrDefaultAsync();
                if (metric != null)
                {
                    metric.LepDateSent = now;
                }
                else
                {
                    invalidIds.Add(metricId);
                }
            }

            await _dbContext.SaveChangesAsync();
            return invalidIds;
        }
    }
}
