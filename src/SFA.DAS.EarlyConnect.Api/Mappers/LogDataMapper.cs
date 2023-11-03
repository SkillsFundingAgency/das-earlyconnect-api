using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
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
                FileName = request.FileName,
                Payload = request.Payload,
                Status = request.Status,
                Error = request.Error
            };

            return log;
        }
    }
}
