using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentAnswerRepository
    {
        Task AddManyAsync(IEnumerable<StudentAnswer> answers);
        Task AddAndRemoveAnswersAsync(Guid studentSurveyId, IEnumerable<StudentAnswer> newAnswers);
        Task UpdateAsync(StudentAnswer answer);
        Task<ICollection<StudentAnswer>> GetStudentAnswerBySurveyIdAsync(Guid surveyId);
    }
}
