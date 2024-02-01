using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class StudentTriageDataOtherPostRequest
    {
        [Required]
        [EmailAddressAttribute(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Invalid LepsCode")]
        public string LepsCode { get; set; }
    }
}
