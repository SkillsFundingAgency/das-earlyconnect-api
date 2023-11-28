using MediatR;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsCode;
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

        public GetMetricsDataByLepsCodeQueryHandler(ILEPSDataRepository lepsDataRepository, IMetricsDataRepository metricsDataRepository, IMediator mediator)
        {
            _lepsDataRepository = lepsDataRepository;
            _metricsDataRepository = metricsDataRepository;
            _mediator = mediator;
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
                foreach (var metricsFlagLookup in metricsFlagLookupList)
                {
                    var metricsFlagDto = new MetricsFlagDto
                    {
                        Flag = metricsFlagLookup.MetricsFlag.FlagName,
                        FlagCode = metricsFlagLookup.MetricsFlag.FlagCode,
                        FlagValue = metricsFlagLookup.FlagValue
                    };
                    metricsFlagDtoList.Add(metricsFlagDto);
                }
            }

            return metricsFlagDtoList;
        }
    }
}
