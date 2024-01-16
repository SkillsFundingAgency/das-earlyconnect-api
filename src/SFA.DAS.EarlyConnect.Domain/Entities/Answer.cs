namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; } // FK to Question
        public virtual Question Question { get; set; } // FK to Question
        public string AnswerText { get; set; }
        public string ShortDescription { get; set; }
        public int GroupNumber { get; set; }
        public int GroupLabel { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}