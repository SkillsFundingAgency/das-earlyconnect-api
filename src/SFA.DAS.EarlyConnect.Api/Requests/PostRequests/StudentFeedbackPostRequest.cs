using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class StudentFeedbackPostRequest
    {
        public IEnumerable<StudentFeedbackRequestModel> ListOfStudentFeedback { get; set; }
    }
}
