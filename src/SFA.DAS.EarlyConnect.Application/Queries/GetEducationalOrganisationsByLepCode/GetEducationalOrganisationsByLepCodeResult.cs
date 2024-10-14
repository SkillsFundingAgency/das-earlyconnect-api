using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode
{
    public class GetEducationalOrganisationsByLepCodeResult
    {
        public int TotalCount { get; set; }
        public ICollection<EducationalOrganisationsDto> EducationalOrganisations { get; set; }
    }
}
