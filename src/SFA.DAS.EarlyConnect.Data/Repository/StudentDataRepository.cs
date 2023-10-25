using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                await _dbContext.AddAsync(student);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
