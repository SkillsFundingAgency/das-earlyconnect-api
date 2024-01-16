using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class StudentTriageDataPostRequest
    {
        public int Id { get; set; }
        public int LepsId { get; set; }
        public int LogId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Postcode { get; set; }
        public string Telephone { get; set; }
        public string DataSource { get; set; }
        public string SchoolName { get; set; }
        public string Industry { get; set; }
        public StudentSurveyRequestModel StudentSurvey { get; set; }
    }
}
