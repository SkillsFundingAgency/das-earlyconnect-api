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

        public async Task UpdateLepsDateSent(IList<int> ids)
        {
            DateTime now = DateTime.Now;

            var students = await _dbContext.SubjectPreferenceData.Where(subjectPreference => ids.Contains(subjectPreference.Id)).ToListAsync();

            students.ForEach(subjectPreference => { subjectPreference.LepDateSent = now; });

            await _dbContext.SaveChangesAsync();
        }
    }
}
