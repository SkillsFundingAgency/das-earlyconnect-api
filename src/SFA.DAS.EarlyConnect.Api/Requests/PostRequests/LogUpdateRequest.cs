using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class LogUpdateRequest
    {
        public int LogId { get; set; }
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }
        public string Error { get; set; }
    }
}
