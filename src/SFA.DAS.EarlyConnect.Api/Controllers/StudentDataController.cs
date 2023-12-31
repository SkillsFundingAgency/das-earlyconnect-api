﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Mappers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using System.Net;

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
        [Route("")]
        public async Task<IActionResult> StudentData([FromBody] StudentDataPostRequest request)
        {
            var command = request.MapFromStudentDataPostRequest();

            await _mediator.Send(new CreateStudentDataCommand
            {
                StudentDataList = command
            });

            return CreatedAtAction(nameof(StudentData), null);
        }
    }
}
