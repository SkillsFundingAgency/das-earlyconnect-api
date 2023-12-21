namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class StudentSurvey
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; } // FK to StudentData
        public virtual StudentData Student { get; set; } // Nav property to StudentData
        public int SurveyId { get; set; } // FK to Survey
        public virtual Survey Survey { get; set; } // Nav property to Survey
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateEmailSet { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}