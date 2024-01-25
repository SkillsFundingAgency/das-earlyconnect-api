﻿namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class AnswerRequestModel
    {
        public int? Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string Response { get; set; }
        public DateTime DateAdded { get; set; }
    }
}