using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using System.Net;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/metrics-data/")]
    public class MetricsDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MetricsDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("")]
        public async Task<IActionResult> MetricsData([FromBody] MetricsDataPostRequest request)
        {
            var command = request.MapFromMetricsDataPostRequest();

            await _mediator.Send(new CreateMetricsDataCommand
            {
                MetricsData = command
            });

            return Ok();
        }
    }
}
