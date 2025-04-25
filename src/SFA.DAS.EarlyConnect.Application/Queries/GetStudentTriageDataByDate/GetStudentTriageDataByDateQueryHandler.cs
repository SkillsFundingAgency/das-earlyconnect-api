using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentTriageDataBySurveyId;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetStudentDataTriageByDate
{
    public class GetStudentDataTriageByDateQueryHandler : IRequestHandler<GetStudentDataTriageByDateQuery, GetStudentDataTriageByDateResult>
    {
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly ILEPSDataRepository _lEPSDataRepository;
        private readonly ILogger<GetStudentDataTriageByDateQueryHandler> _logger;

        public GetStudentDataTriageByDateQueryHandler(
            IStudentDataRepository studentDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository,
            IStudentAnswerRepository studentAnswerRepository,
            ILEPSDataRepository lEPSDataRepository,
            ILogger<GetStudentDataTriageByDateQueryHandler> logger)
        {
            _studentDataRepository = studentDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _studentAnswerRepository = studentAnswerRepository;
            _lEPSDataRepository = lEPSDataRepository;
            _logger = logger;
        }

        public async Task<GetStudentDataTriageByDateResult> Handle(GetStudentDataTriageByDateQuery request, CancellationToken cancellationToken)
        {
            var studentSurveys = await _studentSurveyRepository.GetStudentSurveysForLondonAsync();
            var studentDataTriageDtos = new List<StudentTriageDataDto>();

            foreach (var studentSurvey in studentSurveys)
            {
                var student = await _studentDataRepository.GetByStudentIdAsync(studentSurvey.StudentId);
                if (student == null)
                {
                    _logger.LogWarning($"Student not found for StudentId: {studentSurvey.StudentId}");
                    continue;
                }

                string lepsCode = student.LepsId.HasValue ? await _lEPSDataRepository.GetLepsCodeByLepsIdAsync(student.LepsId.Value) : null;
                var questions = await _questionRepository.GetQuestionBySurveyIdAsync(studentSurvey.SurveyId);
                var studentAnswers = await _studentAnswerRepository.GetStudentAnswerBySurveyIdAsync(studentSurvey.Id);

                var studentTriageDataDto = new StudentTriageDataDto
                {
                    Id = student.Id,
                    LepDateSent = student.LepDateSent,
                    LepsId = student.LepsId,
                    LepCode = lepsCode,
                    LogId = student.LogId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    SchoolName = student.SchoolName,
                    URN = student.URN,
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
                studentDataTriageDtos.Add(studentTriageDataDto);
            }
            return new GetStudentDataTriageByDateResult { StudentTriageData = studentDataTriageDtos };
        }
    }
}