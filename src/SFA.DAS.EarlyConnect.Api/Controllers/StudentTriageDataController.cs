﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
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
        public async Task<IActionResult> StudentTriageData([FromBody] StudentTriageDataOtherPostRequest request)
        {
            var response = await _mediator.Send(new CreateOtherStudentTriageDataCommand
            {
                Email = request.Email,
                LepsCode = request.LepsCode
            });

            return CreatedAtAction(nameof(StudentTriageData), response);
        }
    }
}
