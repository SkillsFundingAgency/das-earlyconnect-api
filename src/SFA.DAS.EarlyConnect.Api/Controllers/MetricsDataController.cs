using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Responses.GetMetricsDataByLepsCode;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Queries.GetMetricsDataByLepsCode;
using SFA.DAS.EarlyConnect.Application.Responses;
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

            var response = await _mediator.Send(new CreateMetricsDataCommand
            {
                MetricsData = command
            });

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest)) 
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("{lepsCode}")]
        public async Task<IActionResult> MetricsData([FromRoute] string lepsCode)
        {
            var queryResult = await _mediator.Send(new GetMetricsDataByLepsCodeQuery
            {
                LEPSCode = lepsCode
            });

            if (queryResult.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = queryResult.ValidationErrors });
            }
            
            return Ok((GetMetricsDataByLepsCodeResponse)queryResult);
        }
    }
}
