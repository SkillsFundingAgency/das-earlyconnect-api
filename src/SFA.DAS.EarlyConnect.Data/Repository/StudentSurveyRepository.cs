using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System.Globalization;

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
            studentSurvey.DateAdded = DateTime.Now;

            await _dbContext.AddAsync(studentSurvey);

            await _dbContext.SaveChangesAsync();

            return studentSurvey.Id;
        }

        public async Task<StudentSurvey> GetByIdAsync(Guid studentSurveyId)
        {
            var studentSurvey = await _dbContext.StudentSurveys.Where(x => x.Id.Equals(studentSurveyId)).SingleOrDefaultAsync();

            if (studentSurvey == null)
            {
                throw new ArgumentException("Cannot find studentSurvey by the supplied ID");
            }

            return studentSurvey;
        }

        public async Task<StudentSurvey> GetStudentSurveyBySurveyIdAsync(Guid surveyId)
        {
            return await _dbContext.StudentSurveys
                .Where(studentSurvey => studentSurvey.Id == surveyId)
                .FirstOrDefaultAsync();
        }


        public async Task<StudentSurvey> GetStudentSurveyByStudentIdAsync(int studentId, int surveyId)
        {
            return await _dbContext.StudentSurveys
                .Where(studentSurvey => studentSurvey.StudentId == studentId && studentSurvey.SurveyId == surveyId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateStudentSurveyAsync(StudentSurvey studentSurveyData)
        {
            var studentSurvey = await _dbContext.StudentSurveys.Where(survey => survey.Id == studentSurveyData.Id).SingleOrDefaultAsync();

            if (studentSurvey == null)
            {
                throw new ArgumentNullException(nameof(studentSurvey), "No Student Survey Found for the supplied ID!");
            }

            studentSurvey.LastUpdated = DateTime.Now;
            studentSurvey.DateCompleted = studentSurveyData.DateCompleted;

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateStudentSurveyReminderEmailDateAsync(Guid? surveyId)
        {
            var studentSurvey = await _dbContext.StudentSurveys.Where(survey => survey.Id == surveyId).SingleOrDefaultAsync();

            if (studentSurvey == null)
            {
                throw new ArgumentNullException(nameof(studentSurvey), "No Student Survey Found for the supplied ID!");
            }

            studentSurvey.DateEmailReminderSent = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        //public async Task<List<StudentSurvey>> GetStudentSurveysForLondonAsync()
        //{
        //    return await _dbContext.StudentSurveys
        //        .Where(studentSurvey => studentSurvey.DateCompleted != null)
        //        .Where(studentSurvey => studentSurvey.DateCompleted < new DateTime(2025, 1, 1))
        //        .ToListAsync();
        //}

        //public async Task<List<StudentSurvey>> GetStudentSurveysForLondonAsync()
        //{
        //    return await _dbContext.StudentSurveys
        //        .Where(studentSurvey => studentSurvey.DateCompleted.HasValue)
        //        .Where(studentSurvey => studentSurvey.DateCompleted.Value < new DateTime(2024, 12, 31))
        //        .ToListAsync();
        //}

        public async Task<List<StudentSurvey>> GetStudentSurveysForLondonAsync()
        {
            DateTime cutoffDate;
            if (!DateTime.TryParseExact("2024-12-31 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out cutoffDate))
            {
                // Handle parsing error (shouldn't happen with a fixed format)
                cutoffDate = new DateTime(2024, 12, 31);
            }

            return await _dbContext.StudentSurveys
                .Where(studentSurvey => studentSurvey.DateCompleted.HasValue)
                .Where(studentSurvey => studentSurvey.DateCompleted.Value < cutoffDate.ToUniversalTime())
                .ToListAsync();
        }

        //public async Task<List<StudentSurvey>> GetStudentSurveysForLondonAsync()
        //{
        //    return await _dbContext.StudentSurveys.Take(10).ToListAsync(); // Retrieve the first 10 rows
        //}

        //public async Task<List<StudentSurvey>> GetStudentSurveysForLondonAsync()
        //{
        //    return await _dbContext.StudentSurveys.Skip(10).Take(10).ToListAsync(); // Retrieve rows 11-20
        //}

    }
}
