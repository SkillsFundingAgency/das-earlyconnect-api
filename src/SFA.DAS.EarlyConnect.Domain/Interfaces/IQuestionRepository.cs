using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IQuestionRepository
    {
        Task<ICollection<Question>> GetQuestionBySurveyIdAsync(int surveyId);
    }
}