using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IAnswerRepository
    {
        Task<ICollection<Answer>> GetAnswerByQuestionIdAsync(int questionId);
    }
}