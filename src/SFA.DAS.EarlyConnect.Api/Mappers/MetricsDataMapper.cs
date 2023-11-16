using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class MetricsDataMapper
    {
        public static ICollection<MetricDto> MapFromMetricsDataPostRequest(this MetricsDataPostRequest request)
        {
            var metrics = new List<MetricDto>();

            foreach (MetricRequestModel model in request.MetricsData)
            {
                var metric = new MetricDto
                {
                    Region = model.Region,
                    IntendedStartYear = model.IntendedStartYear,
                    MaxTravelInMiles = model.MaxTravelInMiles,
                    WillingnessToRelocate = model.WillingnessToRelocate,
                    NoOfGCSCs = model.NoOfGCSCs,
                    NoOfStudents = model.NoOfStudents,
                    LogId = model.LogId,
                    MetricFlags = model.MetricFlags != null
                        ? model.MetricFlags.ToList()
                        : null
                };

                metrics.Add(metric);
            }

            return metrics;
        }
    }
}
