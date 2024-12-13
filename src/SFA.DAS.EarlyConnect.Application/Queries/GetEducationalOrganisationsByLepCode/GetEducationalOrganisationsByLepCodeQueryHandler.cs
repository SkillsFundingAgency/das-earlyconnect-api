using MediatR;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Domain.Dtos;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode
{
    public class GetEducationalOrganisationsByLepCodeQueryHandler : IRequestHandler<GetEducationalOrganisationsByLepCodeQuery, GetEducationalOrganisationsByLepCodeResult>
    {
        private readonly IEducationalOrganisationRepository _educationalOrganisationRepository;

        public GetEducationalOrganisationsByLepCodeQueryHandler(IEducationalOrganisationRepository educationalOrganisationRepository)
        {
            _educationalOrganisationRepository = educationalOrganisationRepository;
        }

        public async Task<GetEducationalOrganisationsByLepCodeResult> Handle(GetEducationalOrganisationsByLepCodeQuery request, CancellationToken cancellationToken)
        {
            var educationalOrganisationsData = await _educationalOrganisationRepository.GetNameByLepCodeAsync(request.LepCode, request.SearchTerm, request.Page, request.PageSize);

            return new GetEducationalOrganisationsByLepCodeResult
            {
                EducationalOrganisations = MapEducationalOrganisationsDto(educationalOrganisationsData),
                TotalCount = educationalOrganisationsData.TotalCount
            };
        }

        private ICollection<EducationalOrganisationsDto> MapEducationalOrganisationsDto(EducationalOrganisationsData educationalOrganisationsData)
        {
            var organisationsDto = new List<EducationalOrganisationsDto>();

            foreach (EducationalOrganisation organisations in educationalOrganisationsData.EducationalOrganisations)
            {
                var organisationDto = new EducationalOrganisationsDto
                {
                    Name = organisations.Name,
                    AddressLine1 = organisations.AddressLine1,
                    Town = organisations.Town,
                    County = organisations.County,
                    PostCode = organisations.PostCode,
                    URN = organisations.URN
                };

                organisationsDto.Add(organisationDto);
            }

            return organisationsDto;
        }
    }
}
