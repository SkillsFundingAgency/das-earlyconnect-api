namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public partial class StudentFeedback
    {
        public int Id { get; set; }
        public int StudentId { get; set; } // FK to StudentData
        public Guid StudentSurveyId { get; set; } // FK to StudentSurvey
        public int LogId { get; set; }
        public string StatusUpdate { get; set; }
        public string Notes { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
