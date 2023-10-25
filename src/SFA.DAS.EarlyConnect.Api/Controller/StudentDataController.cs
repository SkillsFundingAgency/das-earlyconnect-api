using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests;
using SFA.DAS.EarlyConnect.Application.Commands;

namespace SFA.DAS.EarlyConnect.Api.Controller
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
        [Route("")]
        public async Task<IActionResult> Post(StudentDataPostRequest request)
        {
            await _mediator.Send(new CreateStudentDataCommand
            {
                StudentDataList = request.MapFromStudentDataPostRequest()
            });

            return Ok();
        }
    }
}
