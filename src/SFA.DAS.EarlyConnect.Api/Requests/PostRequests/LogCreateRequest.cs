using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class LogCreateRequest
    {
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid RequestType")]
        public string RequestType { get; set; }
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid RequestSource")]
        public string RequestSource { get; set; }

        public string RequestIP { get; set; }
        public string Payload { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9.()-|]+$", ErrorMessage = "Invalid FileName")]
        public string FileName { get; set; }
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }
    }
}
