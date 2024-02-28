using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.DeliveryUpdate;
using SFA.DAS.EarlyConnect.Application.Responses;
using System.Net;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/delivery-update/")]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] DeliveryUpdateRequest request)
        {
            var response = await _mediator.Send(new DeliveryUpdateCommand
            {
                Source = request.Source,
                Ids = request.Ids
            });

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }

            return Ok(response);
        }
    }
}