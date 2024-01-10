using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class StudentRequestModel
    {
        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        [EmailAddressAttribute(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Postcode")]
        public string Postcode { get; set; }
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Industry")]
        public string Industry { get; set; }
        public DateTime? DateOfInterest { get; set; }
        public int LogId { get; set; }
    }
}