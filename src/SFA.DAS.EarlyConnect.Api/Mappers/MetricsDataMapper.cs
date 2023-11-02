using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class MetricsDataMapper
    {
        public static ICollection<ApprenticeMetricsData> MapFromMetricsDataPostRequest(this MetricsDataPostRequest request)
        {
            var metrics = new List<ApprenticeMetricsData>();

            foreach (MetricRequestModel model in request.MetricsData)
            {
                var metric = new ApprenticeMetricsData
                {
                    IntendedStartYear = model.IntendedStartYear,
                    MaxTravelInMiles = model.MaxTravelInMiles,
                    WillingnessToRelocate = model.WillingnessToRelocate,
                    NoOfGCSCs= model.NoOfGCSCs,
                    NoOfStudents= model.NoOfStudents
                };

                metrics.Add(metric);
            }

            return metrics;
        }
    }
}
