
namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<StudentSurvey>? StudentSurveys { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
    }
}
