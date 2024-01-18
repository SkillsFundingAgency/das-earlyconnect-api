﻿using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentAnswerRepository
    {
        Task<ICollection<StudentAnswer>> GetStudentAnswerBySurveyIdAsync(Guid surveyId);
    }
}