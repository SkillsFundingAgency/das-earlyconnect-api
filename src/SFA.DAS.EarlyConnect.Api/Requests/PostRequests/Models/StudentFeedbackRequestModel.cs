﻿using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class StudentFeedbackRequestModel
    {
        [Required]
        public Guid SurveyId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int LogId { get; set; }

        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Status Update, choose from - (CommunicationSent/ReplyAwaiting/ReplyReceived/ActivelyWorking/HelpNoLongerRequired/OfferMade/ContactLost)")]
        public string StatusUpdate { get; set; }

        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Notes")]
        public string Notes { get; set; }

        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Updated by User")]
        public string UpdatedBy { get; set; }
    }
}
