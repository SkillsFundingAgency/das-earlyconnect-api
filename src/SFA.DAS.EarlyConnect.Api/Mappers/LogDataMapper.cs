using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class LogDataMapper
    {
        public static ECAPILog MapFromLogCreateRequest(this LogCreateRequest request)
        {
            var log = new ECAPILog
            {
                RequestSource = request.RequestSource,
                RequestType = request.RequestType,
                RequestIP = request.RequestIP,
                FileName = request.FileName ?? string.Empty,
                Payload = request.Payload,
                Status = request.Status,
                Error = string.Empty
            };

            return log;
        }
    }
}
