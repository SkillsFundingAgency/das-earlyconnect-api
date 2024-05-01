using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentDataRepository
    {
        Task AddManyAsync(IEnumerable<StudentData> studentDataList);
        Task<int> AddStudentDataAsync(StudentData studentData);
        Task<StudentData?> GetByStudentIdAsync(int studentId);
        Task<StudentData?> GetByEmailAsync(string email, string source, int lepsId);
        Task<List<StudentData>> GetByEmailAsync(string email, string source);
        Task<List<StudentData>> GetEmailByLepcodeAsync(string lepcode, string datasource);
        Task UpdateAsync(StudentData studentData);
        Task<List<int>> UpdateLepsDateSent(IList<int> ids);
    }
}