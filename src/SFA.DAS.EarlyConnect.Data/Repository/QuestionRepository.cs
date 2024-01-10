using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public QuestionRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Question>> GetQuestionBySurveyIdAsync(int surveyId)
        {
            return await _dbContext.Questions
                .Where(question => question.SurveyId == surveyId)
                .OrderBy(question => question.SortOrder)
                .ToListAsync();
        }
    }
}
