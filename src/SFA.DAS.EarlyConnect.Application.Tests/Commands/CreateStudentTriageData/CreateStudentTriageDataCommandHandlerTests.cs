using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Data.Repository;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateStudentTriageData
{
    public class CreateStudentTriageDataCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<IStudentAnswerRepository> _mockStudentAnswerRepository;
        public Mock<IStudentDataRepository> _mockStudentDataRepository;
        public Mock<IStudentSurveyRepository> _mockStudentSurveyRepository;
        public Mock<ILogger<CreateStudentTriageDataCommandHandler>> _logger;
        private CreateStudentTriageDataCommandHandler _handler;
        public Mock<IMessageSession> _messageSession;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockStudentAnswerRepository = new Mock<IStudentAnswerRepository>();
            _mockStudentDataRepository = new Mock<IStudentDataRepository>();
            _mockStudentSurveyRepository = new Mock<IStudentSurveyRepository>();
            _logger = new Mock<ILogger<CreateStudentTriageDataCommandHandler>>();
            _messageSession = new Mock<IMessageSession>();

            _handler = new CreateStudentTriageDataCommandHandler(_mockStudentAnswerRepository.Object, 
                _mockStudentDataRepository.Object, 
                _mockStudentSurveyRepository.Object, 
                _logger.Object,
                _messageSession.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task StudentDataNotFound_ExceptionCaught()
        {
            var command = _fixture.Create<CreateStudentTriageDataCommand>();
            var expectedMessage = "No Student ID Found!";

            _mockStudentDataRepository.Setup(repository => repository.UpdateAsync(It.Is<StudentData>(x => x.Id.Equals(command.StudentData.Id))))
                .ThrowsAsync(new ArgumentNullException("student", "No Student ID Found!"));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.Message.Contains(expectedMessage));
        }

        [Test]
        public async Task StudentSurveyNotFound_ExceptionCaught()
        {
            var command = _fixture.Create<CreateStudentTriageDataCommand>();
            var expectedMessage = "No Student Survey Found for the supplied ID";

            _mockStudentDataRepository.Setup(repository => repository.UpdateAsync(It.Is<StudentData>(x => x.Id.Equals(command.StudentData.Id))))
                .Returns(Task.CompletedTask);
            _mockStudentSurveyRepository.Setup(repository => repository.UpdateStudentSurveyAsync(It.IsAny<StudentSurvey>()))
                .ThrowsAsync(new ArgumentNullException("No Student Survey Found for the supplied ID"));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.Message.Contains(expectedMessage));
        }

        [Test]
        public async Task AnswersCreated_ReturnsSuccessMessage()
        {
            var studentAnswerToCreate = _fixture.Create<AnswerDto>();
            var studentAnswersToCreate = new List<AnswerDto>();
            studentAnswersToCreate.Add(studentAnswerToCreate);
            var studentSurveyDto = _fixture.Build<StudentSurveyDto>()
                .With(x => x.Answers, studentAnswersToCreate)
                .Create();
            var command = _fixture.Build<CreateStudentTriageDataCommand>()
                .With(x => x.StudentSurvey, studentSurveyDto)
                .Create();
            var studentSurvey = _fixture.Create<StudentSurvey>();
            var expectedMessage = "Success";

            _mockStudentDataRepository.Setup(repository => repository.UpdateAsync(It.Is<StudentData>(x => x.Id.Equals(command.StudentData.Id))))
                .Returns(Task.CompletedTask);
            _mockStudentSurveyRepository.Setup(repository => repository.GetByIdAsync(It.Is<Guid>(x => x == command.StudentSurveyGuid)))
                .ReturnsAsync(studentSurvey);
            _mockStudentAnswerRepository.Setup(repository => repository.AddManyAsync(It.Is<List<StudentAnswer>>(x => x.First().Response == studentAnswerToCreate.Response)))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.Message.Contains(expectedMessage));
        }
    }
}
