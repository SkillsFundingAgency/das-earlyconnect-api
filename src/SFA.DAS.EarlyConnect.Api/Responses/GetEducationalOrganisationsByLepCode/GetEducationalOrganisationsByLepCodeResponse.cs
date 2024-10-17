using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;

namespace SFA.DAS.EarlyConnect.Api.Responses.GetEducationalOrganisationsByLepCode
{
    public class GetEducationalOrganisationsResponse
    {
        public int TotalCount { get; set; }
        public ICollection<EducationalOrganisation>? EducationalOrganisations { get; set; }

        public static implicit operator GetEducationalOrganisationsResponse(GetEducationalOrganisationsByLepCodeResult r)
        {
            return new GetEducationalOrganisationsResponse
            {
                TotalCount = r.TotalCount,
                EducationalOrganisations = r.EducationalOrganisations
                    .Select(org => (EducationalOrganisation)org)
                    .ToList()
            };
        }
    }
    public class EducationalOrganisation
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string URN { get; set; }

        public static implicit operator EducationalOrganisation(EducationalOrganisationsDto educationalOrganisationsDto)
        {
            return new EducationalOrganisation
            {
                Name = educationalOrganisationsDto.Name,
                AddressLine1 = educationalOrganisationsDto.AddressLine1,
                Town = educationalOrganisationsDto.Town,
                County = educationalOrganisationsDto.County,
                PostCode = educationalOrganisationsDto.PostCode,
                URN = educationalOrganisationsDto.URN
            };
        }
    }
}