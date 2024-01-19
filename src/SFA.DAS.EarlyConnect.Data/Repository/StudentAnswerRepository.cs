using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class StudentAnswerRepository : IStudentAnswerRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public StudentAnswerRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<StudentAnswer>> GetStudentAnswerBySurveyIdAsync(Guid surveyId)
        {
            return await _dbContext.StudentAnswers
                .Where(studentAnswer => studentAnswer.StudentSurveyId == surveyId)
                .ToListAsync();
        }
    }
}
