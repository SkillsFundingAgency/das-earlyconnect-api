using Microsoft.Identity.Client;
using SFA.DAS.EarlyConnect.Domain.Entities;
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
            await _dbContext.AddAsync(log);

            await _dbContext.SaveChangesAsync();

            return log.Id;
        }
    }
}
