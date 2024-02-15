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

        public async Task AddAndRemoveAnswersAsync(Guid studentSurveyId, IEnumerable<StudentAnswer> newAnswers)
        {
            var executionStrategy = _dbContext.Database.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Retrieve and delete existing records for each unique question ID in newAnswers
                        var uniqueQuestionIds = newAnswers.Select(answer => answer.QuestionId).Distinct();

                        foreach (var questionId in uniqueQuestionIds)
                        {
                            var existingAnswers = await _dbContext.StudentAnswers
                                .Where(answer =>
                                    answer.QuestionId == questionId && answer.StudentSurveyId == studentSurveyId)
                                .ToListAsync();

                            if (existingAnswers.Any())
                            {
                                _dbContext.StudentAnswers.RemoveRange(existingAnswers);
                            }
                        }

                        await _dbContext.SaveChangesAsync();

                        // Add new records for each unique question ID in newAnswers
                        foreach (var newAnswer in newAnswers)
                        {
                            newAnswer.DateAdded = DateTime.UtcNow;
                            await _dbContext.AddAsync(newAnswer);
                        }

                        await _dbContext.SaveChangesAsync();

                        // Commit the transaction
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        // Handle exceptions and roll back the transaction if an error occurs
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
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

        public async Task<ICollection<StudentAnswer>> GetStudentAnswerBySurveyIdAsync(Guid surveyId)
        {
            return await _dbContext.StudentAnswers
                .Where(studentAnswer => studentAnswer.StudentSurveyId == surveyId)
                .ToListAsync();
        }
    }
}
