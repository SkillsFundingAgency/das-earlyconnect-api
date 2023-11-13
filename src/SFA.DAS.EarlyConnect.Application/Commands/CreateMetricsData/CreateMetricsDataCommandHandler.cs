using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSData;
using SFA.DAS.EarlyConnect.Application.Queries.GetMetricsFlag;
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

            var metricsFlag = await _mediator.Send(new GetMetricsFlagQuery());

            foreach (MetricDto metricDto in command.MetricsData)
            {
                var lepsId = await _mediator.Send(new GetLEPSDataByRegionQuery
                {
                    Region = metricDto.Region
                });
                if (lepsId > 0)
                {
                    var metrics = new ApprenticeMetricsData
                    {
                        LEPSId = lepsId,
                        IntendedStartYear = metricDto.IntendedStartYear,
                        MaxTravelInMiles = metricDto.MaxTravelInMiles,
                        WillingnessToRelocate = metricDto.WillingnessToRelocate,
                        NoOfGCSCs = metricDto.NoOfGCSCs,
                        NoOfStudents = metricDto.NoOfStudents,
                        LogId = metricDto.LogId,
                        MetricsFlagLookups = new List<ApprenticeMetricsFlagData>() // Initialize the collection
                    };
                    if (metricDto.MetricFlags != null)
                    {
                        foreach (var metricFlag in metricDto.MetricFlags)
                        {
                            var matchingMetricsFlag = metricsFlag.FirstOrDefault(x =>
                                x.FlagCode?.Trim().Replace(" ", "").ToUpperInvariant() == metricFlag?.ToString().Trim().Replace(" ", "").ToUpperInvariant());


                            if (matchingMetricsFlag != null)
                            {
                                var metricsFlagLookup = new ApprenticeMetricsFlagData
                                {
                                    FlagId = matchingMetricsFlag.Id,
                                    FlagValue = true
                                };

                                metrics.MetricsFlagLookups.Add(metricsFlagLookup);
                            }
                            else
                            {
                                _logger.LogWarning($"FlagId not found for FlagCode: {metricFlag}");
                            }
                        }
                    }

                    metricsData.Add(metrics);
                }
            }

            _logger.LogInformation($"Updating metrics data");

            await _metricsDataRepository.AddManyAsync(metricsData);

            return Unit.Value;
        }
    }
}
