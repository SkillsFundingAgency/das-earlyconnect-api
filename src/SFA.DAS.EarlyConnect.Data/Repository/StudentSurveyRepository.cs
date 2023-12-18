using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class StudentSurveyRepository : IStudentSurveyRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public StudentSurveyRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddStudentSurveyAsync(StudentSurvey studentSurvey)
        {
            await _dbContext.AddAsync(studentSurvey);

            await _dbContext.SaveChangesAsync();

            return studentSurvey.Id;
        }
    }
}
