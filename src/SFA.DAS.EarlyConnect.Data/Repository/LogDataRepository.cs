using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Exceptions;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class LogDataRepository : ILogDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public LogDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(ECAPILog log)
        {
            log.DateAdded = DateTime.Now;
            await _dbContext.AddAsync(log);

            await _dbContext.SaveChangesAsync();

            return log.Id;
        }

        public async Task UpdateStatusAndErrorAsync(int logId, string status, string error)
        {
            var log = await _dbContext.ECAPILogs.SingleOrDefaultAsync(log => log.Id == logId);

            if (log == null)
                throw new EntityNotFoundException($"LogId {logId} not found!");

            log.Status = status;
            log.Error = error;
            log.CompletedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}
