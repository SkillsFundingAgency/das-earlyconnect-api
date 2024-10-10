using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Api.Responses.GetEducationalOrganisationsByLepCode
{
    public class GetEducationalOrganisationsByLepCodeResponse
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }

        public static implicit operator  GetEducationalOrganisationsByLepCodeResponse(EducationalOrganisationsDto educationalOrganisationsDto)
        {
            return new GetEducationalOrganisationsByLepCodeResponse
            {
                Name = educationalOrganisationsDto.Name,
                AddressLine1 = educationalOrganisationsDto.AddressLine1,
                Town = educationalOrganisationsDto.Town,
                County = educationalOrganisationsDto.County,
                PostCode = educationalOrganisationsDto.PostCode
            };
        }
    }
}