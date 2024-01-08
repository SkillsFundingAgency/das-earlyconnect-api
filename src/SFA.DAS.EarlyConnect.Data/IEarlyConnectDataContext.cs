using Microsoft.EntityFrameworkCore;
using SFA.DAS.EarlyConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Data
{
    public interface IEarlyConnectDataContext
    {
        DbSet<StudentData> StudentData { get; set; }
        DbSet<ECAPILog> ECAPILogs { get; set; }
        DbSet<LEPSData> LEPSData { get; set; }
        DbSet<LEPSUser> LEPSUser { get; set; }
        DbSet<MetricsFlag> MetricsFlag { get; set; }
        DbSet<ApprenticeMetricsData> MetricsData { get; set; }
        DbSet<StudentSurvey> StudentSurveys { get; set; }
        DbSet<Survey> Surveys { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<QuestionType> QuestionTypes { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<StudentAnswer> StudentAnswers { get; set; }
    }
}
