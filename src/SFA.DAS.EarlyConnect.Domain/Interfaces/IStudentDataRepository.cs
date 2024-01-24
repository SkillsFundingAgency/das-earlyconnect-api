using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentDataRepository
    {
        Task AddManyAsync(IEnumerable<StudentData> studentDataList);
        Task<int> AddStudentDataAsync(StudentData studentData);
        Task<StudentData?> GetByStudentIdAsync(int studentId);
        Task<StudentData?> GetByEmailAsync(string email, string source);
        Task UpdateAsync(StudentData studentData);
    }
}