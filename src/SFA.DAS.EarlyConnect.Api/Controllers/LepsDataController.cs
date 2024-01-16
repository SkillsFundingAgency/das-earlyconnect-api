using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers;
using System.Net;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/leps-data/")]
    public class LepsDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LepsDataController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("")]
        public async Task<IActionResult> LepsDataWithUsers()
        {
            var queryResult = await _mediator.Send(new GetLEPSDataWithUsersQuery { });

            return Ok(queryResult);
        }
    }
}
