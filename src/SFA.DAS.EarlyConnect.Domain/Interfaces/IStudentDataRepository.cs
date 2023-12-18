using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentDataRepository
    {
        Task AddManyAsync(IEnumerable<StudentData> studentDataList);
        Task<int> AddStudentDataAsync(StudentData studentData);
    }
}