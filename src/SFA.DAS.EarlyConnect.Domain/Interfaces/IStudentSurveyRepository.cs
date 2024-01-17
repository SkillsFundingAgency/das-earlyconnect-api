using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentSurveyRepository
    {
        Task<Guid> AddStudentSurveyAsync(StudentSurvey studentSurvey);
        Task UpdateAsync(StudentSurvey studentSurvey);
        Task<StudentSurvey> GetByIdAsync(Guid studentSurveyId);
    }
}
