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
                student.LepDateSent = new DateTime(1900, 01, 01); // dummy LepDateSent
                await _dbContext.AddAsync(student);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
