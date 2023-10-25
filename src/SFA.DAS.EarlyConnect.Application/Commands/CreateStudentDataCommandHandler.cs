using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands
{
    public class CreateStudentDataCommandHandler : IRequestHandler<CreateStudentDataCommand, Unit>
    {
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly ILogger<CreateStudentDataCommandHandler> _logger;

        public CreateStudentDataCommandHandler(
            IStudentDataRepository studentDataRepository, 
            ILogger<CreateStudentDataCommandHandler> logger)
        {
            _studentDataRepository = studentDataRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateStudentDataCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating student data");
            
            await _studentDataRepository.UpsertAsync(command.StudentDataList);

            return Unit.Value;
        }
    }
}
