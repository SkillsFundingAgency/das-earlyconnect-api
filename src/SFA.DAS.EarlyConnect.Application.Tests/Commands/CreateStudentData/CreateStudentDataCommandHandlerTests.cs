using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateStudentData
{
    public class CreateStudentDataCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<IStudentDataRepository> _mockStudentDataRepository;
        public Mock<ILogger<CreateStudentDataCommandHandler>> _logger;
        private CreateStudentDataCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockStudentDataRepository = new Mock<IStudentDataRepository>();
            _logger = new Mock<ILogger<CreateStudentDataCommandHandler>>();
            _handler = new CreateStudentDataCommandHandler(_mockStudentDataRepository.Object, _logger.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Test]
        public async Task SavesStudentData_ReturnsUnitValue()
        {

            // Arrange
            var command = _fixture.Create<CreateStudentDataCommand>();
     

            _mockStudentDataRepository.Setup(repository => repository.AddManyAsync(command.StudentDataList))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockStudentDataRepository.Verify(x => x.AddManyAsync(command.StudentDataList), Times.Once);
        }
    }
}
