using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IStudentSurveyRepository
    {
        Task<Guid> AddStudentSurveyAsync(StudentSurvey studentSurvey);
        Task<StudentSurvey> GetByIdAsync(Guid studentSurveyId);
        Task<StudentSurvey> GetStudentSurveyBySurveyIdAsync(Guid surveyId);
        Task<StudentSurvey> GetStudentSurveyByStudentIdAsync(int studentId, int surveyId);
        Task UpdateStudentSurveyAsync(StudentSurvey studentSurvey);
        Task UpdateStudentSurveyReminderEmailDateAsync(Guid? surveyId);
    }
}
