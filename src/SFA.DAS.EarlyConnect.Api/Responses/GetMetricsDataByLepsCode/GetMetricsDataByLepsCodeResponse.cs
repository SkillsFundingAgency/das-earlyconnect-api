using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetMetricsDataByLepsCode;

namespace SFA.DAS.EarlyConnect.Api.Responses.GetMetricsDataByLepsCode
{
    public class GetMetricsDataByLepsCodeResponse
    {
        public ICollection<ApprenticeshipMetricsDataDto>? ListOfMetricsData { get; set; }

        public static implicit operator GetMetricsDataByLepsCodeResponse(GetMetricsDataByLepsCodeResult r)
        {
            return new GetMetricsDataByLepsCodeResponse
            { 
                ListOfMetricsData = r.ListOfMetricsData 
            };
        }
    }
}
