using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class MetricsDataPostRequest
    {
        public IEnumerable<MetricRequestModel> MetricsData { get; set; }
    }
}
