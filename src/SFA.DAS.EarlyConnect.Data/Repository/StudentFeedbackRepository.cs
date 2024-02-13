using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class StudentFeedbackRepository : IStudentFeedbackRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public StudentFeedbackRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddManyAsync(IEnumerable<StudentFeedback> studentFeedbackList)
        {
            foreach (StudentFeedback feedback in studentFeedbackList)
            {
                feedback.DateAdded = DateTime.Now;
                await _dbContext.AddAsync(feedback);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<StudentFeedback>> GetFeedbackByStudentIdAsync(int studentId)
        {
            return await _dbContext.StudentFeedbacks
                .Where(feedback => feedback.StudentId == studentId)
                .OrderByDescending(feedback => feedback.DateAdded)
                .ToListAsync();
        }
    }
}
