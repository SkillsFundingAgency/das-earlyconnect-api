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
            var educationalOrganisations = await _educationalOrganisationRepository.GetNameByLepCodeAsync(request.LepCode, request.EducationalOrganisationName);

            return new GetEducationalOrganisationsByLepCodeResult
            {
                EducationalOrganisations = MapEducationalOrganisationsDto(educationalOrganisations)
            };
        }

        private ICollection<EducationalOrganisationsDto> MapEducationalOrganisationsDto(ICollection<EducationalOrganisation> educationalOrganisations)
        {
            var organisationsDto = new List<EducationalOrganisationsDto>();

            foreach (EducationalOrganisation organisations in educationalOrganisations)
            {
                var organisationDto = new EducationalOrganisationsDto
                {
                    Name = organisations.Name,
                    AddressLine1 = organisations.AddressLine1,
                    Town = organisations.Town,
                    County = organisations.County,
                    PostCode = organisations.PostCode
                };

                organisationsDto.Add(organisationDto);
            }

            return organisationsDto;
        }
    }
}
