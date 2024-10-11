using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;
using SFA.DAS.EarlyConnect.Api.Requests.GetRequests;
using SFA.DAS.EarlyConnect.Api.Responses.GetEducationalOrganisationsByLepCode;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/educational-organisations-data/")]
    public class EducationalOrganisationDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EducationalOrganisationDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EducationalOrganisationsData([FromQuery] EducationalOrganisationsGetRequest educationalOrganisationsGetRequest)
        {
            var result = await _mediator.Send(new GetEducationalOrganisationsByLepCodeQuery
            {
                LepCode = educationalOrganisationsGetRequest.LepCode,
                SearchTerm = educationalOrganisationsGetRequest.SearchTerm,
                Page = educationalOrganisationsGetRequest.Page,
                PageSize = educationalOrganisationsGetRequest.PageSize
            });

            var response = result.EducationalOrganisations
                .Select(org => (GetEducationalOrganisationsResponse)org)
                .ToList();

            return Ok(response);
        }
    }
}
