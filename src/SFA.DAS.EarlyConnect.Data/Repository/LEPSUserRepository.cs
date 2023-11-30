using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class LEPSUserRepository : ILEPSUserRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public LEPSUserRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<LEPSUser>> GetLepsUsersByLepsIdAsync(int lepsId)
        {
            return await _dbContext.LEPSUser
                .Where(user => user.LepsId == lepsId)
                .ToListAsync();
        }
    }
}
