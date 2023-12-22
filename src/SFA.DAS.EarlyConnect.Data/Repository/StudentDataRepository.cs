using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class StudentDataRepository : IStudentDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public StudentDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddManyAsync(IEnumerable<StudentData> studentDataList)
        {
            foreach (StudentData student in studentDataList) 
            {
                student.DateAdded = DateTime.Now;
                await _dbContext.AddAsync(student);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> AddStudentDataAsync(StudentData studentData)
        {
            studentData.DateAdded = DateTime.Now;
            await _dbContext.AddAsync(studentData);

            await _dbContext.SaveChangesAsync();

            return studentData.Id;
        }

        public async Task<StudentData?> GetByEmailAsync(string email)
        {
            return await _dbContext.StudentData.Where(student => student.Email == email).SingleOrDefaultAsync();
        }
    }
}
