using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSData;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData
{
    public class CreateMetricsDataCommandHandler : IRequestHandler<CreateMetricsDataCommand, Unit>
    {
        private readonly IMetricsDataRepository _metricsDataRepository;
        private readonly ILogger<CreateMetricsDataCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateMetricsDataCommandHandler(
            IMetricsDataRepository metricsDataRepository,
            IMediator mediator,
            ILogger<CreateMetricsDataCommandHandler> logger)
        {
            _metricsDataRepository = metricsDataRepository;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateMetricsDataCommand command, CancellationToken cancellationToken)
        {
            var metricsData = new List<ApprenticeMetricsData>();
            
            foreach (MetricDto metricDto in command.MetricsData) 
            {
                var lepId = await _mediator.Send(new GetLEPSDataByRegionQuery
                {
                    Region = metricDto.Region
                });

                var metrics = new ApprenticeMetricsData
                {
                    LEPSId = lepId,
                    IntendedStartYear = metricDto.IntendedStartYear,
                    MaxTravelInMiles = metricDto.MaxTravelInMiles,
                    WillingnessToRelocate = metricDto.WillingnessToRelocate,
                    NoOfGCSCs = metricDto.NoOfGCSCs,
                    NoOfStudents = metricDto.NoOfStudents
                };

                metricsData.Add(metrics);
            }

            _logger.LogInformation($"Updating metrics data");

            await _metricsDataRepository.AddManyAsync(metricsData);

            return Unit.Value;
        }
    }
}
