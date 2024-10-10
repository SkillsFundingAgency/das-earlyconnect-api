using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;
using SFA.DAS.EarlyConnect.Api.Requests.GetRequests;

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
        public async Task<IActionResult> EducationalOrganisationsData([FromQuery] EducationalOrganisationsGetRequest educationalOrganisationsGetRequest)
        {
            var result = await _mediator.Send(new GetEducationalOrganisationsByLepCodeQuery
            {
                LepCode = educationalOrganisationsGetRequest.LepCode,
                SearchTerm = educationalOrganisationsGetRequest.SearchTerm
            });

            return Ok(result.EducationalOrganisations.Select(c => c).ToList());
        }
    }
}
