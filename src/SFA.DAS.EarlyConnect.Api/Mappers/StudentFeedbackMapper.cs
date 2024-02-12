using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class StudentFeedbackMapper
    {
        public static IEnumerable<StudentFeedback> MapFromStudentFeedbackPostRequest(this StudentFeedbackPostRequest request)
        {
            var listOfStudentFeedback = new List<StudentFeedback>();

            foreach (StudentFeedbackRequestModel dto in request.ListOfStudentFeedback)
            {
                var studentFeedback = new StudentFeedback
                {
                    StudentSurveyId = dto.SurveyId,
                    StatusUpdate = dto.StatusUpdate,
                    Notes = dto.Notes,
                    UpdatedBy = dto.UpdatedBy,
                    StudentId = dto.StudentId,
                    LogId = dto.LogId
                };

                listOfStudentFeedback.Add(studentFeedback);
            }

            return listOfStudentFeedback;
        }
    }
}
