namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public Guid StudentSurveyId { get; set; } // FK to StudentSurvey
        public virtual StudentSurvey StudentSurvey { get; set; } // Nav prop to StudentSurvey
        public int QuestionId { get; set; } // FK to QuestionId
        public virtual Question Question { get; set; } // Nav prop to Question
        public int? AnswerId { get; set; } // FK to AnswerId
        public virtual Answer Answer { get; set; } // Nav prop to AnswerId
        public string Response { get; set; }
        public DateTime DateAdded { get; set; }
    }
}