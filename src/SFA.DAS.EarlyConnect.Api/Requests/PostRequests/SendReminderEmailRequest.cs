using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class SendReminderEmailRequest
    {
        [RegularExpression(@"^(?:[a-zA-Z0-9]+)?$", ErrorMessage = "Invalid LepsCode")]
        public string LepsCode { get; set; }
    }
}
