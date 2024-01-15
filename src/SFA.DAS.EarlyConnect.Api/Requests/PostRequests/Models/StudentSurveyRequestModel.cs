﻿namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class StudentSurveyRequestModel
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public int SurveyId { get; set; }
        public DateTime DateCompleted { get; set; }
        public DateTime DateEmailSent { get; set; }
        public DateTime DateAdded { get; set; }
        public List<AnswerRequestModel> ResponseAnswers { get; set; }
    }
}
