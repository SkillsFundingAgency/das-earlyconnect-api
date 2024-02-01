using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class AnswerRequestModel
    {
        public int? Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        [RegularExpression(@"^[\w\s|.!&%(),;+-]+$", ErrorMessage = "Invalid Response")]
        public string Response { get; set; }
    }
}
