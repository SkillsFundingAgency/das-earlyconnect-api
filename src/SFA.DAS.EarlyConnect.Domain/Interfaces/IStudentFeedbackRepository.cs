using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentFeedbackRepository
    {
        Task AddManyAsync(IEnumerable<StudentFeedback> studentFeedbackList);
        Task<ICollection<StudentFeedback>> GetFeedbackByStudentIdAsync(int studentId);
    }
}
