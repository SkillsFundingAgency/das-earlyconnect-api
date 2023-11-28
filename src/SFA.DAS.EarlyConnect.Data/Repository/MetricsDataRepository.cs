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

        public async Task AddManyAsync(IEnumerable<ApprenticeMetricsData> metricsData)
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var metricsDataToDelete = _dbContext.MetricsData.Where(metrics => metrics.IsDeleted);
                        _dbContext.MetricsData.RemoveRange(metricsDataToDelete);

                        await _dbContext.SaveChangesAsync();

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
    }
}
