﻿using Microsoft.EntityFrameworkCore;
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
                throw new ArgumentException("Cannot find student survey by the supplied ID");
            }

            return studentSurvey;
        }
    }
}
