using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class SchoolsLeadsDataRepository : ISchoolsLeadsDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public SchoolsLeadsDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateLepsDateSent(IList<int> ids)
        {
            DateTime now = DateTime.Now;

            var students = await _dbContext.SchoolsLeadsData.Where(lead => ids.Contains(lead.Id)).ToListAsync();

            students.ForEach(lead => { lead.LepDateSent = now; });

            await _dbContext.SaveChangesAsync();
        }

    }
}
