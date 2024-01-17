using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class StudentSurveyMapper
    {
        public static StudentSurveyDto MapFromStudentSurveyRequest(this StudentSurveyRequestModel request)
        {
            var studentSurvey = new StudentSurveyDto
            {
                Id = request.Id,
                StudentId = request.StudentId,
                SurveyId = request.SurveyId,
                Answers = request.ResponseAnswers.MapFromAnswersRequest()
            };

            return studentSurvey;
        }
    }
}
