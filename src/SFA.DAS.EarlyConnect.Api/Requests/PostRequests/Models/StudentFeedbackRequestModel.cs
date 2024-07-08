using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class StudentFeedbackRequestModel
    {
        [Required]
        public Guid SurveyId { get; set; }

        [Required]
        public int LogId { get; set; }

        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Status Update")]
        public string StatusUpdate { get; set; }

        public string Notes { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}
