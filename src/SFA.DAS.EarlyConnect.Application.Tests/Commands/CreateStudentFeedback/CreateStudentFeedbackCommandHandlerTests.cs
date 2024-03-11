using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentFeedback;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentIdBySurveyId;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateStudentFeedback
{
    public class CreateStudentFeedbackCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<IStudentFeedbackRepository> _mockStudentFeedbackRepository;
        public Mock<ILogger<CreateStudentFeedbackCommandHandler>> _logger;
        private CreateStudentFeedbackCommandHandler _handler;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockStudentFeedbackRepository = new Mock<IStudentFeedbackRepository>();
            _logger = new Mock<ILogger<CreateStudentFeedbackCommandHandler>>();
            _mediatorMock = new Mock<IMediator>();

            _handler = new CreateStudentFeedbackCommandHandler(_mockStudentFeedbackRepository.Object, _logger.Object, _mediatorMock.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task SavesStudentFeedback_ReturnsUnitValue()
        {
            var command = _fixture.Create<CreateStudentFeedbackCommand>();

            foreach (var student in command.StudentFeedbackList)
            {
                student.StatusUpdate = "CommunicationSent";
            }

            _mockStudentFeedbackRepository.Setup(repository => repository.AddManyAsync(command.StudentFeedbackList))
                .Returns(Task.CompletedTask);

            _mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetStudentIdBySurveyIdQuery>(), CancellationToken.None))
                .ReturnsAsync(123);

            await _handler.Handle(command, CancellationToken.None);

            _mockStudentFeedbackRepository.Verify(x => x.AddManyAsync(command.StudentFeedbackList), Times.Once);
        }
    }
}
