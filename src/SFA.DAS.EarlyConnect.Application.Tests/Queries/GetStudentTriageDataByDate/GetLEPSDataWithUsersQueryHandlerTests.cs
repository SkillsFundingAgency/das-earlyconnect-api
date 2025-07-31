using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentDataTriageByDate;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetStudentTriageDataByDate
{
    [TestFixture]
    public class GetStudentTriageDataByDate
    {
        private Fixture _fixture;
        public Mock<IStudentDataRepository> _studentDataRepository;
        public Mock<IQuestionRepository> _questionRepository;
        public Mock<IStudentSurveyRepository> _studentSurveyRepository;
        public Mock<IStudentAnswerRepository> _studentAnswerRepository;
        public Mock<ILEPSDataRepository> _lEPSDataRepository;
        public Mock<IAnswerRepository> _answerRepository;
        public Mock<IMediator> _mediator;
        public Mock<ILogger<GetStudentDataTriageByDateQueryHandler>> mockLogger;
        private GetStudentDataTriageByDateQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _studentDataRepository = new Mock<IStudentDataRepository>();
            _lEPSDataRepository = new Mock<ILEPSDataRepository>();
            _questionRepository = new Mock<IQuestionRepository>();
            _answerRepository = new Mock<IAnswerRepository>();
            
            _studentSurveyRepository = new Mock<IStudentSurveyRepository>();
            _studentAnswerRepository = new Mock<IStudentAnswerRepository>();
            _answerRepository = new Mock<IAnswerRepository>();
            _mediator = new Mock<IMediator>();
            mockLogger = new Mock<ILogger<GetStudentDataTriageByDateQueryHandler>>();
            _handler = new GetStudentDataTriageByDateQueryHandler(
                _studentDataRepository.Object,
                _studentSurveyRepository.Object,
                _questionRepository.Object,
                _answerRepository.Object,
                _studentAnswerRepository.Object,
                _lEPSDataRepository.Object,
                mockLogger.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task RetrievesStudentTriageData_ReturnsStudentTriageData()
        {
            var query = new GetStudentDataTriageByDateQuery { };

            var studentData = _fixture.Create<StudentData>();
            var questionList = _fixture.Create<ICollection<Question>>();
            var studentSurveys = _fixture.Create<List<StudentSurvey>>();
            var studentAnswer = _fixture.Create<ICollection<StudentAnswer>>();
            var answer = _fixture.Create<ICollection<Answer>>();

            _studentDataRepository.Setup(repo => repo.GetByStudentIdAsync(It.IsAny<int>()))
                .ReturnsAsync(studentData);

            _questionRepository.Setup(repo => repo.GetQuestionBySurveyIdAsync(It.IsAny<int>()))
                .ReturnsAsync(questionList);

            _studentSurveyRepository.Setup(repo => repo.GetStudentSurveysForLondonAsync(It.IsAny<DateTime>(),It.IsAny<DateTime>()))
                .ReturnsAsync(studentSurveys);

            _studentAnswerRepository.Setup(repo => repo.GetStudentAnswerBySurveyIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(studentAnswer);

            _answerRepository.Setup(repo => repo.GetAnswerByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(answer);
            
            _mediator.Setup(x => x.Send(It.IsAny<GetStudentDataTriageByDateQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<GetStudentDataTriageByDateResult>());

            var result = await _handler.Handle(query, CancellationToken.None);

            _answerRepository.Verify(x => x.GetAnswerByQuestionIdAsync(It.IsAny<int>()), Times.AtLeastOnce);
            _studentSurveyRepository.Verify(x => x.GetStudentSurveysForLondonAsync(It.IsAny<DateTime>(),It.IsAny<DateTime>()), Times.Once);
            
            Assert.That(result, Is.InstanceOf<GetStudentDataTriageByDateResult>());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StudentTriageData, Is.Not.Null);
        }
    }
}