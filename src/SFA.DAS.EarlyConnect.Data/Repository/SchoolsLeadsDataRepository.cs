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

        public async Task<List<int>> UpdateLepsDateSent(IList<int> ids)
        {
            DateTime now = DateTime.Now;
            var invalidIds = new List<int>();

            foreach (var leadId in ids)
            {
                var lead = await _dbContext.SchoolsLeadsData.Where(lead => lead.Id == leadId).FirstOrDefaultAsync();
                if (lead != null)
                {
                    lead.LepDateSent = now;
                }
                else
                {
                    invalidIds.Add(leadId);
                }
            }

            await _dbContext.SaveChangesAsync();
            return invalidIds;
        }
    }
}
