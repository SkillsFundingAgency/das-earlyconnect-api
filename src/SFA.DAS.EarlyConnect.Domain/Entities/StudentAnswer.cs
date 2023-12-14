namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public int StudentSurveyId { get; set; } // FK to StudentSurvey
        public StudentSurvey StudentSurvey { get; set; } // Nav prop to StudentSurvey
        public int QuestionId { get; set; } // FK to QuestionId
        public Question Question { get; set; } // Nav prop to Question
        public int? AnswerId { get; set; } // FK to AnswerId
        public Answer Answer { get; set; } // Nav prop to AnswerId
        public string Response { get; set; }
        public DateTime DateAdded { get; set; }
    }
}