﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentData;
using SFA.DAS.EarlyConnect.Api.Responses.SendReminderEmail;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentTriageDataBySurveyId;
using SFA.DAS.EarlyConnect.Application.Responses;
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

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }
            return CreatedAtAction(nameof(StudentTriageDataOther), response);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Route("reminder")]
        public async Task<IActionResult> StudentSurveyEmailReminder([FromBody] SendReminderEmailRequest request)
        {
            var response = await _mediator.Send(new SendReminderEmailCommand
            {
                LepsCode = request.LepsCode
            });

            if (response.ResultCode.Equals(ResponseCode.InvalidRequest))
            {
                return BadRequest(new { Errors = response.ValidationErrors });
            }

            var model = new SendReminderEmailResponse()
            {
                Message = response.Message
            };

            return CreatedAtAction(nameof(StudentSurveyEmailReminder), model);
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
                    DataSource = (request.DataSource != null) ? request.DataSource : "UCAS",
                    SchoolName = request.SchoolName,
                    URN = request.URN,
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
        [Route("{studentSurveyGuid}")]
        public async Task<IActionResult> StudentTriageData([FromRoute] Guid studentSurveyGuid)
        {
            var queryResult = await _mediator.Send(new GetStudentTriageDataBySurveyIdQuery
            {
                StudentSurveyId = studentSurveyGuid
            });

            return Ok(queryResult.StudentTriageData);
        }
    }
}
