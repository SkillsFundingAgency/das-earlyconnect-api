using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class StudentTriageDataPostRequest
    {
        public int Id { get; set; }

        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [EmailAddressAttribute(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Postcode")]
        public string Postcode { get; set; }


        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Telephone")]
        public string Telephone { get; set; }

        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Data Source")]
        public string DataSource { get; set; }

        [RegularExpression(@"^[\w\s&-]+$", ErrorMessage = "Invalid SchoolName")]
        public string SchoolName { get; set; }

        [RegularExpression(@"^[\w\s|]+$", ErrorMessage = "Invalid Industry")]
        public string Industry { get; set; }

        public StudentSurveyRequestModel StudentSurvey { get; set; }
    }
}
