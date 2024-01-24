using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentTriageDataBySurveyId;
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("{studentSurveyGuid}")]
        public async Task<IActionResult> StudentTriageData([FromBody] StudentTriageDataPostRequest request, [FromRoute] Guid studentSurveyGuid)
        {
            var studentSurvey = request.StudentSurvey.MapFromStudentSurveyRequest();

            var result = await _mediator.Send(new CreateStudentTriageDataCommand
            {
                StudentSurveyGuid = studentSurveyGuid,
                StudentData = new StudentDataDto 
                { 
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Email = request.Email,
                    Postcode = request.Postcode,
                    Telephone = request.Telephone,
                    DataSource = "UCAS",
                    SchoolName = request.SchoolName,
                    Industry = request.Industry
                },
                StudentSurvey = studentSurvey
            });

            var response = new CreateStudentDataResponse()
            {
                Message = result.Message
            };

            if (response.Message != "Success") 
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(StudentTriageData), response);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("{surveyGuid}")]
        public async Task<IActionResult> StudentTriageData([FromRoute] string surveyGuid)
        {
            var queryResult = await _mediator.Send(new GetStudentTriageDataBySurveyIdQuery
            {
                StudentSurveyId = surveyGuid
            });

            return Ok(queryResult.StudentTriageData);
        }
    }
}
