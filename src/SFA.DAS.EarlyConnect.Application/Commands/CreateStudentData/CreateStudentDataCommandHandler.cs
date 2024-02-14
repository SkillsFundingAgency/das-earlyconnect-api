using Azure;
using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsPostCode;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData
{
    public class CreateStudentDataCommandHandler : IRequestHandler<CreateStudentDataCommand, CreateStudentDataResult>
    {
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly ILogger<CreateStudentDataCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateStudentDataCommandHandler(
            IStudentDataRepository studentDataRepository,
            ILogger<CreateStudentDataCommandHandler> logger,
            IMediator mediator)
        {
            _studentDataRepository = studentDataRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<CreateStudentDataResult> Handle(CreateStudentDataCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating student data");

            CreateStudentDataResult createStudentDataResult = new CreateStudentDataResult();

            var validationErrors = new List<DetailedValidationError>(); 

            if (command.StudentDataList.Any(s => s.DateOfBirth >= DateTime.Now.Date))
            {
                _logger.LogInformation($"Invalid Date of Birth found!");

                createStudentDataResult.ResultCode = ResponseCode.InvalidRequest;
                validationErrors.Add(new DetailedValidationError()
                {
                    Field = "DateOfBirth", 
                    Message= "Invalid Date of Birth in File"
                });
            }

            if (command.StudentDataList.Any(s => s.DateInterestShown > DateTime.Now.Date)) 
            {
                _logger.LogInformation($"Invalid Date Interest Shown found!");

                createStudentDataResult.ResultCode = ResponseCode.InvalidRequest;
                validationErrors.Add(new DetailedValidationError()
                {
                    Field = "DateInterestShown",
                    Message = "Invalid Date Interest Shown in File"
                });
            }

            if(createStudentDataResult.ResultCode == ResponseCode.InvalidRequest) 
            {
                createStudentDataResult.ValidationErrors = validationErrors.Cast<object>().ToList();
                return createStudentDataResult;
            }

            foreach (var studentData in command.StudentDataList)
            {
                var lepsId = await _mediator.Send(new GetLEPSDataByLepsPostCodeQuery
                {
                    PostCode = studentData.Postcode
                });

                if (lepsId == 0)
                {
                    _logger.LogInformation($"Postcode Unmatched with LEPCode: {studentData.Postcode}");

                    createStudentDataResult.Message = string.IsNullOrEmpty(createStudentDataResult.Message)
                        ? $"Postcode Unmatched with LEPCode {studentData.Email}-{studentData.Postcode}"
                        : $"{createStudentDataResult.Message} , {studentData.Email}-{studentData.Postcode}";
                }
                else
                {
                    studentData.LepsId = lepsId;
                }
            }

            await _studentDataRepository.AddManyAsync(command.StudentDataList);

            return createStudentDataResult;
        }
    }
}
