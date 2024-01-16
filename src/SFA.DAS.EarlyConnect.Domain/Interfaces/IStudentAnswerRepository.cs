using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentAnswerRepository
    {
        Task AddManyAsync(IEnumerable<StudentAnswer> answers);
        Task UpdateAsync(StudentAnswer answer);
    }
}
