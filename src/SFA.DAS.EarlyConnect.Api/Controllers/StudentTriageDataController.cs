using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Models;
using System.Net;

namespace SFA.DAS.EarlyConnect.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/student-triage-data/")]
    public class StudentTriageDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentTriageDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Route("survey-create")]
        public async Task<IActionResult> StudentTriageDataOther([FromBody] StudentTriageDataOtherPostRequest request)
        {
            var response = await _mediator.Send(new CreateOtherStudentTriageDataCommand
            {
                Email = request.Email,
                LepsCode = request.LepsCode
            });

            return CreatedAtAction(nameof(StudentTriageDataOther), response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Route("{studentSurveyGuid}")]
        public async Task<IActionResult> StudentTriageData([FromBody] StudentTriageDataPostRequest request, [FromRoute] Guid studentSurveyGuid)
        {
            var studentSurvey = request.StudentSurvey.MapFromStudentSurveyRequest();

            var response = await _mediator.Send(new CreateStudentTriageDataCommand
            {
                StudentSurveyGuid = studentSurveyGuid,
                StudentData = new StudentDataDto 
                { 
                    Id = request.Id,
                    LepsId = request.LepsId,
                    LogId = request.LogId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    Postcode = request.Postcode,
                    DataSource = request.DataSource,
                    Industry = request.Industry,
                    DateOfInterest = request.DateOfInterest
                },
                StudentSurvey = studentSurvey
            });

            return CreatedAtAction(nameof(StudentTriageData), response);
        }
    }
}
