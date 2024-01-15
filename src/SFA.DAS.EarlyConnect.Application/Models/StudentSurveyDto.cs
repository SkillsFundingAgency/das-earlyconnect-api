namespace SFA.DAS.EarlyConnect.Application.Models
{
    public class StudentSurveyDto
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public int SurveyId { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateEmailSent { get; set; }
        public DateTime DateAdded { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
