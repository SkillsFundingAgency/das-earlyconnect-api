using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests
{
    public class StudentDataPostRequest
    {
        public IEnumerable<StudentRequestModel> ListOfStudentData { get; set; }
    }
}
