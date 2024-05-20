using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.EarlyConnect.Domain.Entities;
using System.Data;

namespace SFA.DAS.EarlyConnect.Data
{
    public class EarlyConnectDataContext : DbContext, IEarlyConnectDataContext
    {
        private const string AzureResource = "https://database.windows.net/";
        private readonly ChainedTokenCredential _azureServiceTokenProvider;
        private readonly EnvironmentConfiguration _environmentConfiguration;
        private readonly EarlyConnectApiConfiguration? _configuration;

        public DbSet<StudentData> StudentData { get; set; }
        public DbSet<ECAPILog> ECAPILogs { get; set; }
        public DbSet<LEPSData> LEPSData { get; set; }
        public DbSet<LEPSUser> LEPSUser { get; set; }
        public DbSet<MetricsFlag> MetricsFlag { get; set; }
        public DbSet<ApprenticeMetricsData> MetricsData { get; set; }
        public DbSet<StudentSurvey> StudentSurveys { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<StudentFeedback> StudentFeedbacks { get; set; }
        public DbSet<SchoolsLeadsData> SchoolsLeadsData { get; set; }
        public DbSet<SubjectPreferenceData> SubjectPreferenceData { get; set; }

        public EarlyConnectDataContext()
        {

        }

        public EarlyConnectDataContext(DbContextOptions options) : base(options)
        {

        }

        public EarlyConnectDataContext(IOptions<EarlyConnectApiConfiguration> config, DbContextOptions options, ChainedTokenCredential azureServiceTokenProvider, EnvironmentConfiguration environmentConfiguration) : base(options)
        {
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _environmentConfiguration = environmentConfiguration;
            _configuration = config.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentData>().ToTable("StudentData");
            modelBuilder.Entity<StudentData>().HasKey(student => student.Id);
            //modelBuilder.Entity<StudentData>().HasOne(student => student.LEPSData)
            //           .WithMany(lEPSData => lEPSData.StudentData)
            //.HasForeignKey(student => student.LepsId);

            modelBuilder.Entity<ECAPILog>().ToTable("ECAPILog");
            modelBuilder.Entity<ECAPILog>().HasKey(log => log.Id);
            modelBuilder.Entity<ECAPILog>().HasMany(log => log.StudentData)
                .WithOne(student => student.Log)
                .HasForeignKey(lookup => lookup.LogId);
            modelBuilder.Entity<ECAPILog>().HasMany(log => log.ApprenticeMetricsData)
                .WithOne(metric => metric.Log)
                .HasForeignKey(metric => metric.LogId);

            modelBuilder.Entity<ApprenticeMetricsData>().ToTable("ApprenticeMetricsData");
            modelBuilder.Entity<ApprenticeMetricsData>().HasKey(m => m.Id);
            modelBuilder.Entity<ApprenticeMetricsData>().HasMany(m => m.MetricsFlagLookups)
                .WithOne(lookup => lookup.MetricsData)
                .HasForeignKey(lookup => lookup.MetricsId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApprenticeMetricsFlagData>().ToTable("ApprenticeMetricsFlagData");
            modelBuilder.Entity<ApprenticeMetricsFlagData>().HasKey(x => x.Id);

            modelBuilder.Entity<MetricsFlag>().ToTable("MetricsFlag");
            modelBuilder.Entity<MetricsFlag>().HasKey(x => x.Id);
            modelBuilder.Entity<MetricsFlag>().HasMany(flag => flag.MetricsFlagLookups)
                .WithOne(lookup => lookup.MetricsFlag)
                .HasForeignKey(lookup => lookup.FlagId);

            modelBuilder.Entity<LEPSData>().ToTable("LEPSData");
            modelBuilder.Entity<LEPSData>().HasKey(d => d.Id);
            modelBuilder.Entity<LEPSData>().HasMany(d => d.LEPSUsers)
                .WithOne(user => user.LepsData)
                .HasForeignKey(d => d.LepsId);
            modelBuilder.Entity<LEPSData>().HasMany(d => d.LEPSCoverages)
                .WithOne(coverage => coverage.LepsData)
                .HasForeignKey(d => d.LEPSId);

            modelBuilder.Entity<StudentSurvey>().ToTable("StudentSurvey");
            modelBuilder.Entity<StudentSurvey>().HasKey(studentSurvey => studentSurvey.Id);
            modelBuilder.Entity<StudentSurvey>().HasMany(studentSurvey => studentSurvey.StudentAnswers)
                .WithOne(studentAnswer => studentAnswer.StudentSurvey)
                .HasForeignKey(studentAnswer => studentAnswer.StudentSurveyId);

            modelBuilder.Entity<StudentAnswer>().ToTable("StudentAnswer");
            modelBuilder.Entity<StudentAnswer>().HasKey(studentAnswer => studentAnswer.Id);

            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Answer>().HasKey(answer => answer.Id);
            modelBuilder.Entity<Answer>().HasMany(answer => answer.StudentAnswers)
                .WithOne(studentAnswer => studentAnswer.Answer)
                .HasForeignKey(studentAnswer => studentAnswer.AnswerId);

            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Question>().HasKey(question => question.Id);
            modelBuilder.Entity<Question>().HasMany(question => question.Answers)
                .WithOne(answer => answer.Question)
                .HasForeignKey(answer => answer.QuestionId);
            modelBuilder.Entity<Question>().HasMany(question => question.StudentAnswers)
                .WithOne(studentAnswer => studentAnswer.Question)
                .HasForeignKey(studentAnswer => studentAnswer.QuestionId);

            modelBuilder.Entity<QuestionType>().ToTable("QuestionType");
            modelBuilder.Entity<QuestionType>().HasKey(questionType => questionType.Id);
            modelBuilder.Entity<QuestionType>().HasMany(questionType => questionType.Questions)
                .WithOne(question => question.QuestionType)
                .HasForeignKey(question => question.QuestionTypeId);

            modelBuilder.Entity<Survey>().ToTable("Survey");
            modelBuilder.Entity<Survey>().HasKey(survey => survey.Id);
            modelBuilder.Entity<Survey>().HasMany(survey => survey.StudentSurveys)
                .WithOne(studentSurvey => studentSurvey.Survey)
                .HasForeignKey(studentSurvey => studentSurvey.SurveyId);
            modelBuilder.Entity<Survey>().HasMany(survey => survey.Questions)
                .WithOne(question => question.Survey)
                .HasForeignKey(question => question.SurveyId);

            modelBuilder.Entity<StudentFeedback>().ToTable("StudentFeedback");
            modelBuilder.Entity<StudentFeedback>().HasKey(feedback => feedback.Id);
            modelBuilder.Entity<StudentFeedback>().Property(e => e.DateAdded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            modelBuilder.Entity<StudentFeedback>().Property(e => e.Notes)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

            modelBuilder.Entity<StudentFeedback>().Property(e => e.StatusUpdate)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

            modelBuilder.Entity<StudentFeedback>().Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

            modelBuilder.Entity<SchoolsLeadsData>().ToTable("SchoolsLeadsData");
            modelBuilder.Entity<SchoolsLeadsData>().HasKey(schoolsLeads => schoolsLeads.Id);

            modelBuilder.Entity<SubjectPreferenceData>().ToTable("SubjectPreferenceData");
            modelBuilder.Entity<SubjectPreferenceData>().HasKey(subject => subject.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            if (_configuration == null) return;

            var connection = new SqlConnection
            {
                ConnectionString = _configuration.DatabaseConnectionString
            };

            if (!(_environmentConfiguration.EnvironmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)
                || _environmentConfiguration.EnvironmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase)))
            {
                connection.AccessToken = _azureServiceTokenProvider
                    .GetTokenAsync(new TokenRequestContext(scopes: new string[] { AzureResource })).Result.Token;
            }

            optionsBuilder.UseSqlServer(connection, options =>
                options.EnableRetryOnFailure(
                    5,
                    TimeSpan.FromSeconds(20),
                    null
                ));
        }
    }
}
