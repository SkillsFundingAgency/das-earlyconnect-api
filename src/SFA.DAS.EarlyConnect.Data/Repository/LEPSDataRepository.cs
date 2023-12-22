using Microsoft.EntityFrameworkCore;
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
            return await _dbContext.LEPSData
                .Where(a => a.Region != null && a.Region.Trim().Replace(" ", "").ToLower() == region.Trim().Replace(" ", "").ToLower())
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

        }

        public async Task<int> GetLepsIdByLepsCodeAsync(string lepsCode)
        {
            return await _dbContext.LEPSData
                .Where(a => a.LepCode == lepsCode)
                .Select(l => l.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<int> GetLepsIdByPostCodeAsync(string postCode)
        {
            postCode = postCode.Trim().Replace(" ", "").ToLower();

            return await _dbContext.LEPSData
                .Where(a => a.LEPSCoverages.Any(LEPSCoverage => !LEPSCoverage.IsDeleted && LEPSCoverage.Postcode == postCode))
                .Select(l => l.Id)
                .SingleOrDefaultAsync();
        }


        public async Task<string> GetLepsRegionByLepsCodeAsync(string lepsCode)
        {
            return await _dbContext.LEPSData
                .Where(a => a.LepCode == lepsCode)
                .Select(l => l.Region)
                .SingleOrDefaultAsync();
        }

        public async Task<ICollection<LEPSData>> GetAllLepsDataAsync()
        {
            return await _dbContext.LEPSData.ToListAsync();
        }
    }
}
