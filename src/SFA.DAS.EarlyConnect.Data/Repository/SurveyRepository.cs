using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public SurveyRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Survey> GetDefaultSurveyAsync()
        {
            return await _dbContext.Surveys.FirstOrDefaultAsync();
        }
    }
}
