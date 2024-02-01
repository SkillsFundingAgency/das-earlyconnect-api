using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public AnswerRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Answer>> GetAnswerByQuestionIdAsync(int questionId)
        {
            return await _dbContext.Answers
                .Where(answer => answer.QuestionId == questionId)
                .OrderBy(answer => answer.SortOrder)
                .ToListAsync();
        }
    }
}
