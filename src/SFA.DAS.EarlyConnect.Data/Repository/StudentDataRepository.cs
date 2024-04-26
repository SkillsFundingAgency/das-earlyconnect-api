﻿using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
namespace SFA.DAS.EarlyConnect.Data.Repository
{
    public class StudentDataRepository : IStudentDataRepository
    {
        private readonly EarlyConnectDataContext _dbContext;

        public StudentDataRepository(EarlyConnectDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddManyAsync(IEnumerable<StudentData> studentDataList)
        {
            foreach (StudentData student in studentDataList)
            {
                student.DateAdded = DateTime.Now;
                await _dbContext.AddAsync(student);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> AddStudentDataAsync(StudentData studentData)
        {
            studentData.DateAdded = DateTime.Now;
            await _dbContext.AddAsync(studentData);

            await _dbContext.SaveChangesAsync();

            return studentData.Id;
        }

        public async Task<StudentData?> GetByEmailAsync(string email, string source, int lepsId)
        {
            return await _dbContext.StudentData.Where(student => student.Email == email && student.DataSource == source && student.LepsId == lepsId).FirstOrDefaultAsync();
        }

        public async Task<List<StudentData>> GetByEmailAsync(string email, string source)
        {
            return await _dbContext.StudentData
                .Where(student => student.Email == email && student.DataSource == source)
                .ToListAsync();
        }

        public async Task<List<StudentData>> GetEmailByLepcodeAsync(string lepcode, string datasource)
        {
            lepcode = lepcode.Trim().Replace(" ", "").ToLower();
            datasource = datasource.Trim().Replace(" ", "").ToLower();

            return await _dbContext.StudentData
                 .AsNoTracking()
                 .Include(a => a.LEPSData)
                 .Include(a => a.StudentSurveys)
                .Where(a => a.LEPSData.LepCode == lepcode
                         && a.DataSource == datasource
                         && a.StudentSurveys != null
                         && a.StudentSurveys.Any(s => s.DateEmailReminderSent == null && s.DateCompleted == null && s.DateAdded < DateTime.Now.Date.AddDays(-2)))
                .ToListAsync();
        }

        public async Task UpdateAsync(StudentData studentData)
        {
            var student = await _dbContext.StudentData.Where(student => studentData.Id == student.Id).SingleOrDefaultAsync();

            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "No Student ID Found!");
            }

            student.FirstName = studentData.FirstName;
            student.LastName = studentData.LastName;
            student.DateOfBirth = studentData.DateOfBirth;
            student.Email = studentData.Email;
            student.SchoolName = (studentData.SchoolName != null) ? studentData.SchoolName : "";
            student.Postcode = (studentData.Postcode != null) ? studentData.Postcode : "";
            student.Telephone = (studentData.Telephone != null) ? studentData.Telephone : "";
            student.Industry = (studentData.Industry != null) ? studentData.Industry : "";

            await _dbContext.SaveChangesAsync();
        }

        public async Task<StudentData?> GetByStudentIdAsync(int studentId)
        {
            return await _dbContext.StudentData.Where(student => student.Id == studentId).SingleOrDefaultAsync();
        }

        public async Task<List<int>> UpdateLepsDateSent(IList<int> ids)
        {
            DateTime now = DateTime.Now;
            var invalidIds = new List<int>();

            foreach (var studentId in ids)
            {
                var student = await _dbContext.StudentData.Where(student => student.Id == studentId).FirstOrDefaultAsync();
                if (student != null)
                {
                    student.LepDateSent = now;
                }
                else
                {
                    invalidIds.Add(studentId);
                }
            }

            await _dbContext.SaveChangesAsync();
            return invalidIds;
        }
    }
}
