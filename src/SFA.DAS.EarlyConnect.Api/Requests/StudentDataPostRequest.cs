using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.EarlyConnect.Api.Requests
{
    public class StudentDataPostRequest
    {
        public IEnumerable<StudentDataDto> ListOfStudentData { get; set; }
    }
}
