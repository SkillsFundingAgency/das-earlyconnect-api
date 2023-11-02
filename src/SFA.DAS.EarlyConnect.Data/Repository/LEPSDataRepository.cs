using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class LEPSDataRepository : ILEPSDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public LEPSDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetLepsIdByRegionAsync(string region)
        {
            return await _dbContext.LEPSData.Where(a => a.Region == region)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }
    }
}
