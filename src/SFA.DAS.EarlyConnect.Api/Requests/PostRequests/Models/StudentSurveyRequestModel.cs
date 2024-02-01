namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class StudentSurveyRequestModel
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public int SurveyId { get; set; }
        public List<AnswerRequestModel> ResponseAnswers { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
