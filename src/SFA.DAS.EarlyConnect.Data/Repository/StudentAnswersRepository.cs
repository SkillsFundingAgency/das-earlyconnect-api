using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class StudentAnswersRepository : IStudentAnswerRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public StudentAnswersRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddManyAsync(IEnumerable<StudentAnswer> answers)
        {
            foreach (var answer in answers)
            {
                answer.DateAdded = DateTime.UtcNow;
                await _dbContext.AddAsync(answer);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentAnswer answerToUpdate)
        {
            var studentAnswer = await _dbContext.StudentAnswers.Where(x => x.Id.Equals(answerToUpdate.Id)).SingleOrDefaultAsync();

            if (studentAnswer == null)
            {
                throw new ArgumentException(nameof(studentAnswer), "Cannot find student answer by the supplied ID");
            }

            studentAnswer.StudentSurveyId = answerToUpdate.StudentSurveyId;
            studentAnswer.QuestionId = answerToUpdate.QuestionId;
            studentAnswer.AnswerId = answerToUpdate.AnswerId;
            studentAnswer.Response = answerToUpdate.Response;

            await _dbContext.SaveChangesAsync();
        }
    }
}
