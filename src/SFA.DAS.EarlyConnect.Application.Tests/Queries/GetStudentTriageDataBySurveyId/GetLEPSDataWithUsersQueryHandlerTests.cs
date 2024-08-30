using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentTriageDataBySurveyId;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetStudentTriageDataBySurveyId
{
    [TestFixture]
    public class GetStudentTriageDataBySurveyId
    {
        private Fixture _fixture;
        public Mock<IStudentDataRepository> _studentDataRepository;
        public Mock<IQuestionRepository> _questionRepository;
        public Mock<IStudentSurveyRepository> _studentSurveyRepository;
        public Mock<IStudentAnswerRepository> _studentAnswerRepository;
        public Mock<ILEPSDataRepository> _lEPSDataRepository;
        public Mock<IAnswerRepository> _answerRepository;
        public Mock<IMediator> _mediator;
        public Mock<ILogger<GetStudentTriageDataBySurveyIdQueryHandler>> mockLogger;
        private GetStudentTriageDataBySurveyIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _studentDataRepository = new Mock<IStudentDataRepository>();
            _lEPSDataRepository = new Mock<ILEPSDataRepository>();
            _questionRepository = new Mock<IQuestionRepository>();
            _studentSurveyRepository = new Mock<IStudentSurveyRepository>();
            _studentAnswerRepository = new Mock<IStudentAnswerRepository>();
            _answerRepository = new Mock<IAnswerRepository>();
            _mediator = new Mock<IMediator>();
            mockLogger = new Mock<ILogger<GetStudentTriageDataBySurveyIdQueryHandler>>();
            _handler = new GetStudentTriageDataBySurveyIdQueryHandler(
                _studentDataRepository.Object,
                _questionRepository.Object,
                _studentSurveyRepository.Object,
                _studentAnswerRepository.Object,
                _lEPSDataRepository.Object,
                _answerRepository.Object,
                mockLogger.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task RetrievesStudentTriageData_ReturnsStudentTriageData()
        {
            var query = new GetStudentTriageDataBySurveyIdQuery { };

            var studentData = _fixture.Create<StudentData>();
            var questionList = _fixture.Create<ICollection<Question>>();
            var studentSurvey = _fixture.Create<StudentSurvey>();
            var studentAnswer = _fixture.Create<ICollection<StudentAnswer>>();
            var answer = _fixture.Create<ICollection<Answer>>();

            _studentDataRepository.Setup(repo => repo.GetByStudentIdAsync(It.IsAny<int>()))
                .ReturnsAsync(studentData);

            _questionRepository.Setup(repo => repo.GetQuestionBySurveyIdAsync(It.IsAny<int>()))
                .ReturnsAsync(questionList);

            _studentSurveyRepository.Setup(repo => repo.GetStudentSurveyBySurveyIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(studentSurvey);

            _studentAnswerRepository.Setup(repo => repo.GetStudentAnswerBySurveyIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(studentAnswer);

            _answerRepository.Setup(repo => repo.GetAnswerByQuestionIdAsync(It.IsAny<int>()))
                .ReturnsAsync(answer);

            _mediator.Setup(x => x.Send(It.IsAny<GetStudentTriageDataBySurveyIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<GetStudentTriageDataBySurveyIdResult>());

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(result, Is.InstanceOf<GetStudentTriageDataBySurveyIdResult>());

            _studentDataRepository.Verify(x => x.GetByStudentIdAsync(It.IsAny<int>()), Times.Once);
            _questionRepository.Verify(x => x.GetQuestionBySurveyIdAsync(It.IsAny<int>()), Times.Once);
            _studentSurveyRepository.Verify(x => x.GetStudentSurveyBySurveyIdAsync(It.IsAny<Guid>()), Times.Once);
            _studentAnswerRepository.Verify(x => x.GetStudentAnswerBySurveyIdAsync(It.IsAny<Guid>()), Times.Once);
            _answerRepository.Verify(x => x.GetAnswerByQuestionIdAsync(It.IsAny<int>()), Times.AtLeastOnce);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StudentTriageData, Is.Not.Null);
        }
    }
}
