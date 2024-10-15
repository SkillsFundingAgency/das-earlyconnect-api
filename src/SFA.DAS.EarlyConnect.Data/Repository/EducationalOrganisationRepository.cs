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

        public async Task<EducationalOrganisationsData> GetNameByLepCodeAsync(string lepCode, string? name, int page, int pageSize)
        {
            IQueryable<Domain.Entities.EducationalOrganisation> query = _dbContext.EducationalOrganisation
                .Where(educationalOrganisation => educationalOrganisation.LepCode == lepCode
                                                  && (string.IsNullOrEmpty(name)
                                                      || (educationalOrganisation.Name != null
                                                          && educationalOrganisation.Name.ToLower().Contains(name.ToLower()))))
                .OrderBy(educationalOrganisation => educationalOrganisation.Name);

            int totalCount = await query.CountAsync();

            if (page > 0 && pageSize > 0)
            {
                query = query.Skip((page - 1) * pageSize)
                    .Take(pageSize);
            }

            var results = await query.Select(educationalOrganisation => new EducationalOrganisation
            {
                Name = educationalOrganisation.Name,
                AddressLine1 = educationalOrganisation.AddressLine1,
                County = educationalOrganisation.County,
                Town = educationalOrganisation.Town,
                URN = educationalOrganisation.URN,
                PostCode = educationalOrganisation.PostCode
            }).ToListAsync();

            return new EducationalOrganisationsData
            {
                EducationalOrganisations = results,
                TotalCount = totalCount
            };
        }
    }
}
