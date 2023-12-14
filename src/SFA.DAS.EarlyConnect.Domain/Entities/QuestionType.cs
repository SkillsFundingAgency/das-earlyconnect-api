namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class QuestionType
    {
        public int Id { get; set; }
        public string QuestionTypeText { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<Question>? Questions { get; set; }
    }
}