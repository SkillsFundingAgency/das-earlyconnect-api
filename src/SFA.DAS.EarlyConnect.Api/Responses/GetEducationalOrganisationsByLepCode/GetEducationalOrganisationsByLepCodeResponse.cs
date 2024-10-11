﻿using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Api.Responses.GetEducationalOrganisationsByLepCode
{
    public class GetEducationalOrganisationsResponse
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string URN { get; set; }

        public static implicit operator GetEducationalOrganisationsResponse(EducationalOrganisationsDto educationalOrganisationsDto)
        {
            return new GetEducationalOrganisationsResponse
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