using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentFeedback;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentFeedback;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers;
using SFA.DAS.EarlyConnect.Application.Responses;
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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("student-feedback")]
        public async Task<IActionResult> StudentFeedback([FromBody] StudentFeedbackPostRequest request)
        {
            var command = request.MapFromStudentFeedbackPostRequest();

            var response = await _mediator.Send(new CreateStudentFeedbackCommand
            {
                StudentFeedbackList = command
            });

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }

            var model = new CreateStudentFeedbackResponse()
            {
                Message = response.Message
            };

            return CreatedAtAction(nameof(StudentFeedback), model);
        }
    }
}
