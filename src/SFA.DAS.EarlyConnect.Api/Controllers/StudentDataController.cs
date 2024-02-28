using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using System.Net;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentData;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentOnboardData;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentOnboardData;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/student-data/")]
    public class StudentDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("")]
        public async Task<IActionResult> StudentData([FromBody] StudentDataPostRequest request)
        {
            var command = request.MapFromStudentDataPostRequest();

            var response = await _mediator.Send(new CreateStudentDataCommand
            {
                StudentDataList = command
            });

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }

            var model = new CreateStudentDataResponse()
            {
                Message = response.Message
            };

            return CreatedAtAction(nameof(StudentData), model);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("onboard")]
        public async Task<IActionResult> StudentOnboardData([FromBody] StudentOnboardDataPostRequest request)
        {
            var response = await _mediator.Send(new CreateStudentOnboardDataCommand
            {
                Emails = request.Emails,
            });

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }

            var model = new CreateStudentOnboardDataResponse()
            {
                Message = response.Message
            };

            return CreatedAtAction(nameof(StudentData), model);
        }
    }
}
