using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class LogUpdateRequest
    {
        public int LogId { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
}
