using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetStudentTriageDataBySurveyId
{
    public class GetStudentTriageDataBySurveyIdQueryHandler : IRequestHandler<GetStudentTriageDataBySurveyIdQuery, GetStudentTriageDataBySurveyIdResult>
    {
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly ILEPSDataRepository _lEPSDataRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ILogger<GetStudentTriageDataBySurveyIdQueryHandler> _logger;

        public GetStudentTriageDataBySurveyIdQueryHandler(
            IStudentDataRepository studentDataRepository,
            IQuestionRepository questionRepository,
            IStudentSurveyRepository studentSurveyRepository,
            IStudentAnswerRepository studentAnswerRepository,
            ILEPSDataRepository lEPSDataRepository,
            IAnswerRepository answerRepository,
            ILogger<GetStudentTriageDataBySurveyIdQueryHandler> logger)
        {
            _studentDataRepository = studentDataRepository;
            _questionRepository = questionRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _studentAnswerRepository = studentAnswerRepository;
            _lEPSDataRepository = lEPSDataRepository;
            _answerRepository = answerRepository;
            _logger = logger;
        }

        public async Task<GetStudentTriageDataBySurveyIdResult> Handle(GetStudentTriageDataBySurveyIdQuery request, CancellationToken cancellationToken)
        {
            string lepsCode=String.Empty;

            var studentSurvey = await _studentSurveyRepository.GetStudentSurveyBySurveyIdAsync(request.StudentSurveyId);
            if (studentSurvey == null) _logger.LogInformation($"No StudentSurveyId found for the SurveyId  {request.StudentSurveyId}");

            var studentAnswers = await _studentAnswerRepository.GetStudentAnswerBySurveyIdAsync(studentSurvey.Id);

            var student = await _studentDataRepository.GetByStudentIdAsync(studentSurvey.StudentId);
            if (student == null) _logger.LogInformation($"No student found for the StudentId  {studentSurvey.StudentId}");

            if (student.LepsId.HasValue)
            {
                lepsCode = await _lEPSDataRepository.GetLepsCodeByLepsIdAsync(student.LepsId.Value);
                if (string.IsNullOrEmpty(lepsCode)) _logger.LogInformation($"No leps Code found for the Leps Id {student.LepsId.Value}");
            }
            else
            {
                _logger.LogInformation("LepsId is null for the student");
            }
            
            var questions = await _questionRepository.GetQuestionBySurveyIdAsync(studentSurvey.SurveyId);
            if (questions == null || !questions.Any()) _logger.LogInformation($"No questions found for the SurveyId  {studentSurvey.SurveyId}");


            var studentTriageDataDto = new StudentTriageDataDto
            {
                Id = student.Id,
                LepsId = student.LepsId,
                LepCode = lepsCode,
                LogId = student.LogId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                SchoolName = student.SchoolName,
                Email = student.Email,
                Telephone = student.Telephone,
                Postcode = student.Postcode,
                DataSource = student.DataSource,
                Industry = student.Industry,
                DateInterest = student.DateInterestShown,
                StudentSurvey = new StudentSurveyDto
                {
                    Id = studentSurvey.Id,
                    StudentId = studentSurvey.StudentId,
                    SurveyId = studentSurvey.SurveyId,
                    LastUpdated = studentSurvey.LastUpdated,
                    DateCompleted = studentSurvey.DateCompleted,
                    DateEmailSent = studentSurvey.DateEmailSent,
                    DateAdded = studentSurvey.DateAdded,
                    ResponseAnswers = studentAnswers.Select(sa => new ResponseAnswersDto
                    {
                        Id = sa.Id,
                        StudentSurveyId = sa.StudentSurveyId,
                        QuestionId = sa.QuestionId,
                        AnswerId = sa.AnswerId,
                        Response = sa.Response,
                        DateAdded = sa.DateAdded
                    }).ToList()
                },
                SurveyQuestions = questions.Select(question => new SurveyQuestionsDto
                {
                    Id = question.Id,
                    SurveyId = question.SurveyId,
                    QuestionTypeId = question.QuestionTypeId,
                    QuestionText = question.QuestionText,
                    ShortDescription = question.ShortDescription,
                    SummaryLabel = question.SummaryLabel,
                    ValidationMessage = question.ValidationMessage,
                    GroupLabel = question.GroupLabel,
                    GroupNumber = question.GroupNumber,
                    DefaultToggleAnswerId = question.DefaultToggleAnswerId,
                    SortOrder = question.SortOrder,
                    Answers = _answerRepository.GetAnswerByQuestionIdAsync(question.Id)
                        .Result
                        .Select(answer => new AnswersDto
                        {
                            Id = answer.Id,
                            QuestionId = answer.QuestionId,
                            AnswerText = answer.AnswerText,
                            ShortDescription = answer.ShortDescription,
                            GroupLabel = answer.GroupLabel,
                            GroupNumber = answer.GroupNumber,
                            SortOrder = answer.SortOrder,
                        }).ToList()
                }).ToList()
            };

            return new GetStudentTriageDataBySurveyIdResult
            {
                StudentTriageData = studentTriageDataDto
            };
        }
    }
}
