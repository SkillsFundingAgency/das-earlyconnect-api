using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode
{
    public class GetEducationalOrganisationsByLepCodeQuery : IRequest<GetEducationalOrganisationsByLepCodeResult>
    {
        public string LepCode { get; set; }
        public string? SearchTerm { get; set; }
    }
}
