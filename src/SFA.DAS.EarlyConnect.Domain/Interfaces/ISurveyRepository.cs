using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ISurveyRepository
    {
        Task<Survey> GetDefaultSurveyAsync();
        Task<ICollection<Survey>> GetSurveyBySurveyIdAsync(int surveyId);
    }
}
