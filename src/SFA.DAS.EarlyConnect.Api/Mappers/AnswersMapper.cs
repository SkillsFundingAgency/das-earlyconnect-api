using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class AnswersMapper
    {
        public static List<AnswerDto> MapFromAnswersRequest(this ICollection<AnswerRequestModel> request)
        {
            var answers = new List<AnswerDto>();

            foreach (AnswerRequestModel model in request)
            {
                var answer = new AnswerDto
                {
                    Id = model.Id,
                    StudentSurveyId = model.StudentSurveyId,
                    AnswerId = model.AnswerId,
                    QuestionId = model.QuestionId,
                    Response = model.Response,
                    DateAdded = model.DateAdded
                };

                answers.Add(answer);
            }

            return answers;
        }
    }
}
