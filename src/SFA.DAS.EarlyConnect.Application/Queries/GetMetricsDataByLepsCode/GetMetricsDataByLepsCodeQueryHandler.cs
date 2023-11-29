using MediatR;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsCode;
using SFA.DAS.EarlyConnect.Application.Queries.GetMetricsFlag;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetMetricsDataByLepsCode
{
    public class GetMetricsDataByLepsCodeQueryHandler : IRequestHandler<GetMetricsDataByLepsCodeQuery, GetMetricsDataByLepsCodeResult>
    {
        private readonly ILEPSDataRepository _lepsDataRepository;
        private readonly IMetricsDataRepository _metricsDataRepository;
        private readonly IMediator _mediator;
        private IEnumerable<MetricsFlag> _allMetricsFlags { get; set; }

        public GetMetricsDataByLepsCodeQueryHandler(ILEPSDataRepository lepsDataRepository, IMetricsDataRepository metricsDataRepository, IMediator mediator)
        {
            _lepsDataRepository = lepsDataRepository;
            _metricsDataRepository = metricsDataRepository;
            _mediator = mediator;
            _allMetricsFlags = new List<MetricsFlag>();
        }

        public async Task<GetMetricsDataByLepsCodeResult> Handle(GetMetricsDataByLepsCodeQuery request, CancellationToken cancellationToken)
        {
            var lepsData = await _mediator.Send(new GetLEPSDataByLepsCodeQuery
            {
                LEPSCode = request.LEPSCode
            });

            if (lepsData.LepId == 0)
            {
                return new GetMetricsDataByLepsCodeResult
                {
                    ResultCode = ResponseCode.InvalidRequest,
                    ValidationErrors = new List<DetailedValidationError>
                                {
                                    new DetailedValidationError
                                    {
                                        Field = "LepCode", Message = "Cannot get LEPSData by the provided LepCode"
                                    }
                                }.Cast<object>().ToList()
                };
            }

            _allMetricsFlags = await _mediator.Send(new GetMetricsFlagQuery());

            var metricsData = await _metricsDataRepository.GetByLepsIdAsync(lepsData.LepId);

            return new GetMetricsDataByLepsCodeResult
            {
                ResultCode = ResponseCode.Success,
                ListOfMetricsData = MapToMetricsDataListDto(metricsData, lepsData.Region)
            };
        }

        private ApprenticeshipMetricsDataDto MapToMetricsDataDto(ApprenticeMetricsData metricsData, string region) 
        {
            return new ApprenticeshipMetricsDataDto
            {
                Id = metricsData.Id,
                Region = region,
                IntendedStartYear = metricsData.IntendedStartYear,
                WillingnessToRelocate = metricsData.WillingnessToRelocate,
                NoOfGCSCs = metricsData.NoOfGCSCs,
                NoOfStudents = metricsData.NoOfStudents,
                MaxTravelInMiles = metricsData.MaxTravelInMiles,
                LogId = metricsData.LogId,
                DateAdded = metricsData.DateAdded,
                InterestAreas = MapToMetricsFlagDto(metricsData.MetricsFlagLookups)
            };
        }

        private ICollection<ApprenticeshipMetricsDataDto> MapToMetricsDataListDto(ICollection<ApprenticeMetricsData>? metricsDataList, string region)
        {
            var metricsDtoList = new List<ApprenticeshipMetricsDataDto>();
            
            if (metricsDataList != null) 
            {
                foreach (var metricsData in metricsDataList)
                {
                    var metricsDto = MapToMetricsDataDto(metricsData, region);
                    metricsDtoList.Add(metricsDto);
                }
            }

            return metricsDtoList;
        }

        private IList<MetricsFlagDto> MapToMetricsFlagDto(ICollection<ApprenticeMetricsFlagData>? metricsFlagLookupList)
        {
            var metricsFlagDtoList = new List<MetricsFlagDto>();

            if (metricsFlagLookupList != null)
            {
                foreach (var flag in _allMetricsFlags)
                {
                    if (metricsFlagLookupList.Any(lookup => lookup.FlagId == flag.Id))
                    {
                        var metricsFlagDto = new MetricsFlagDto
                        {
                            Flag = flag.FlagName,
                            FlagCode = flag.FlagCode,
                            FlagValue = true
                        };
                        metricsFlagDtoList.Add(metricsFlagDto);
                    }
                    else 
                    {
                        var metricsFlagDto = new MetricsFlagDto
                        {
                            Flag = flag.FlagName,
                            FlagCode = flag.FlagCode,
                            FlagValue = false
                        };
                        metricsFlagDtoList.Add(metricsFlagDto);
                    }
                }
            }

            return metricsFlagDtoList;
        }
    }
}
