using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Responses;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetMetricsDataByLepsCode
{
    public class GetMetricsDataByLepsCodeResult : BaseResponse
    {
        public ICollection<ApprenticeshipMetricsDataDto>? ListOfMetricsData { get; set;}
    }
}
