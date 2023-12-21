﻿using SFA.DAS.EarlyConnect.Domain.Enums;

namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int SurveyId { get; set; } // FK to Survey
        public Survey Survey { get; set; } // Nav property to Survey
        public int QuestionTypeId { get; set; } // FK to QuestionType
        public QuestionType QuestionType { get; set; } // Nav property to QuestionType
        public string QuestionText { get; set; }
        public string ShortDescription { get; set; }
        public string SummaryLabel { get; set; }
        public int? DefaultToggleAnswerId { get; set; }
        public SortOrder SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
        public virtual ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}