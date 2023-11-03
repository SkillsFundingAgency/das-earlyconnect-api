using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using System.Net;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/log/")]
    public class LogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("add")]
        public async Task<IActionResult> Create([FromBody] LogCreateRequest request)
        {
            var command = request.MapFromLogCreateRequest();

            await _mediator.Send(new CreateLogCommand
            {
                Log = command
            });

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] LogUpdateRequest request)
        {
            await _mediator.Send(new UpdateLogCommand
            {
                LogId = request.LogId,
                Status = request.Status,
                Error = request.Error
            });

            return Ok(); 

        }
    }
}