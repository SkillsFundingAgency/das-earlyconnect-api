using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Dtos;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class EducationalOrganisationRepository : IEducationalOrganisationRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public EducationalOrganisationRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EducationalOrganisation>> GetNameByLepCodeAsync(string lepCode, string? name)
        {
            return await _dbContext.EducationalOrganisation
                .Where(educationalOrganisation => educationalOrganisation.LepCode == lepCode
                                                  && (string.IsNullOrEmpty(name)
                                                      || (educationalOrganisation.Name != null
                                                          && educationalOrganisation.Name.ToLower().Contains(name.ToLower())))) // Convert to lowercase for case-insensitive comparison
                .Select(educationalOrganisation => new EducationalOrganisation
                {
                    Name = educationalOrganisation.Name,
                    AddressLine1 = educationalOrganisation.AddressLine1,
                    Town = educationalOrganisation.Town
                })
                .ToListAsync();
        }

    }
}
