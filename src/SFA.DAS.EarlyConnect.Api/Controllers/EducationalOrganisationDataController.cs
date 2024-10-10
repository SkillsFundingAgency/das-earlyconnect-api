using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/educational-organisation-data/")]
    public class EducationalOrganisationDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EducationalOrganisationDataController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("{lepCode}")]
        public async Task<IActionResult> EducationalOrganisationData([FromRoute] String lepCode, String educationalOrganisationName)
        {
            var queryResult = await _mediator.Send(new GetEducationalOrganisationsByLepCodeQuery
            {
                LepCode = lepCode,
                EducationalOrganisationName = educationalOrganisationName
            });

            return Ok(queryResult.EducationalOrganisations);
        }
    }
}
