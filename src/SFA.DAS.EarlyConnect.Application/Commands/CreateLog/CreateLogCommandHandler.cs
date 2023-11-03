using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateLog
{
    public class CreateLogCommandHandler : IRequestHandler<CreateLogCommand, int>
    {
        private readonly ILogDataRepository _logDataRepository;
        private readonly ILogger<CreateLogCommandHandler> _logger;

        public CreateLogCommandHandler(
            ILogDataRepository metricsDataRepository,
            ILogger<CreateLogCommandHandler> logger)
        {
            _logDataRepository = metricsDataRepository;
            _logger = logger;
        }

        public async Task<int> Handle(CreateLogCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating log data");

            var logId = await _logDataRepository.CreateAsync(command.Log);

            return logId;
        }
    }
}
