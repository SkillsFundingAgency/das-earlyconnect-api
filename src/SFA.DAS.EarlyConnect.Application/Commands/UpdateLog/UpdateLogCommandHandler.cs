using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Domain.Exceptions;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.UpdateLog
{
    public class UpdateLogCommandHandler : IRequestHandler<UpdateLogCommand, bool>
    {
        private readonly ILogDataRepository _logDataRepository;
        private readonly ILogger<UpdateLogCommandHandler> _logger;

        public UpdateLogCommandHandler(
            ILogDataRepository metricsDataRepository,
            ILogger<UpdateLogCommandHandler> logger)
        {
            _logDataRepository = metricsDataRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateLogCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Updating log data...");
                await _logDataRepository.UpdateStatusAndErrorAsync(command.LogId, command.Status, command.Error);
                return true;
            }
            catch (EntityNotFoundException exc)
            {
                _logger.LogInformation(exc.Message);
                return false;
            }
        }
    }
}
