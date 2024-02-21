using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class SubjectPreferenceDataRepository : ISubjectPreferenceDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public SubjectPreferenceDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<int>> UpdateLepsDateSent(IList<int> ids)
        {
            DateTime now = DateTime.Now;
            var invalidIds = new List<int>();

            foreach (var preferenceId in ids)
            {
                var metric = await _dbContext.SubjectPreferenceData.Where(preference => preference.Id == preferenceId).FirstOrDefaultAsync();
                if (metric != null)
                {
                    metric.LepDateSent = now;
                }
                else
                {
                    invalidIds.Add(preferenceId);
                }
            }

            await _dbContext.SaveChangesAsync();
            return invalidIds;
        }
    }
}
