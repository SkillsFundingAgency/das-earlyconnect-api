using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsPostCode;
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
