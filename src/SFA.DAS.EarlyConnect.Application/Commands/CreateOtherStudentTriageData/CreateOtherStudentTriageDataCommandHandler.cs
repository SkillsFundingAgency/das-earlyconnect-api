using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByRegion;
using SFA.DAS.EarlyConnect.Application.Queries.GetMetricsFlag;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;
using SFA.DAS.EarlyConnect.Data.Repository;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData
{
    public class CreateOtherStudentTriageDataCommandHandler : IRequestHandler<CreateOtherStudentTriageDataCommand, CreateOtherStudentTriageDataCommandResponse>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly ILEPSDataRepository _lepsDataRepository;    
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly IDataProtectorService _dataProtectorService;
        private readonly ILogger<CreateOtherStudentTriageDataCommandHandler> _logger;

        public CreateOtherStudentTriageDataCommandHandler(
            ISurveyRepository surveyRepository,
            IStudentDataRepository studentDataRepository,
            ILEPSDataRepository lepsDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            IDataProtectorService dataProtectorService,
            ILogger<CreateOtherStudentTriageDataCommandHandler> logger)
        {
            _surveyRepository = surveyRepository;
            _studentDataRepository = studentDataRepository;
            _lepsDataRepository = lepsDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _dataProtectorService = dataProtectorService;
            _logger = logger;
        }

        public async Task<CreateOtherStudentTriageDataCommandResponse> Handle(CreateOtherStudentTriageDataCommand command, CancellationToken cancellationToken)
        {
            // 0. Fetch the first or default survey record
            var survey = await _surveyRepository.GetDefaultSurveyAsync();

            // get lpsId by lepsCode
            var lepsId = await _lepsDataRepository.GetLepsIdByLepsCodeAsync(command.LepsCode);

            // 1. Create Student Data (dummy)
            var studentData = new StudentData
            {
                Email = command.Email,
                LepsId = lepsId
            };

            var studentId = await _studentDataRepository.AddStudentDataAsync(studentData);

            // 2. Create Student Survey
            var studentSurvey = new StudentSurvey
            {
                StudentId = studentId,
                SurveyId = survey.Id,
                DateAdded = DateTime.UtcNow
            };

            var studentSurveyId = await _studentSurveyRepository.AddStudentSurveyAsync(studentSurvey);

            _logger.LogInformation($"Student Survey created for student with email {studentData.Email}");

            // 3. Generate auth code using the TriageAuthService
            var authCode = _dataProtectorService.EncodedData(studentSurveyId, studentData.Email);

            return new CreateOtherStudentTriageDataCommandResponse
            {
                StudentSurveyId = studentSurveyId.ToString(),
                AuthCode = authCode
            };
        }
    }
}
