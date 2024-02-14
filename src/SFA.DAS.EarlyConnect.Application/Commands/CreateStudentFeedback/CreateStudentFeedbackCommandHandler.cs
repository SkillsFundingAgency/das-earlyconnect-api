using Azure;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsPostCode;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentIdBySurveyId;
using SFA.DAS.EarlyConnect.Application.Requests;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentFeedback
{
    public class CreateStudentFeedbackCommandHandler : IRequestHandler<CreateStudentFeedbackCommand, CreateStudentFeedbackResult>
    {
        private readonly IStudentFeedbackRepository _studentFeedbackRepository;
        private readonly ILogger<CreateStudentFeedbackCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateStudentFeedbackCommandHandler(
            IStudentFeedbackRepository studentFeedbackRepository,
            ILogger<CreateStudentFeedbackCommandHandler> logger,
            IMediator mediator)
        {
            _studentFeedbackRepository = studentFeedbackRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<CreateStudentFeedbackResult> Handle(CreateStudentFeedbackCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating student feedback");

            CreateStudentFeedbackResult createStudentFeedbackResult = new CreateStudentFeedbackResult();

            var validationErrors = new List<DetailedValidationError>();

            var statusList = Enum.GetNames(typeof(StatusUpdate));

            if (command.StudentFeedbackList.Any(s => !statusList.Contains(s.StatusUpdate)))
            {
                _logger.LogInformation($"Invalid Status Update!");

                createStudentFeedbackResult.ResultCode = ResponseCode.InvalidRequest;
                validationErrors.Add(new DetailedValidationError()
                {
                    Field = "StatusUpdate",
                    Message = "Invalid Status Update, choose from - (CommunicationSent/ReplyAwaiting/ReplyReceived/ActivelyWorking/HelpNoLongerRequired/OfferMade/ContactLost)"
                });
            }

            foreach (var studentFeedback in command.StudentFeedbackList)
            {
                var studentId = await _mediator.Send(new GetStudentIdBySurveyIdQuery
                {
                    StudentSurveyId = studentFeedback.StudentSurveyId
                });

                if (studentId == 0)
                {
                    _logger.LogInformation($"Invalid Survey ID: {studentFeedback.StudentSurveyId}");

                    createStudentFeedbackResult.Message = string.IsNullOrEmpty(createStudentFeedbackResult.Message)
                        ? $"Invalid Survey ID: {studentFeedback.StudentSurveyId}"
                        : $"{createStudentFeedbackResult.Message} , {studentFeedback.StudentSurveyId}";
                }
                else
                {
                    studentFeedback.StudentId = studentId;
                }
            }

            if (createStudentFeedbackResult.ResultCode == ResponseCode.InvalidRequest)
            {
                createStudentFeedbackResult.ValidationErrors = validationErrors.Cast<object>().ToList();
                return createStudentFeedbackResult;
            }            

            await _studentFeedbackRepository.AddManyAsync(command.StudentFeedbackList);

            return createStudentFeedbackResult;
        }
    }
}
