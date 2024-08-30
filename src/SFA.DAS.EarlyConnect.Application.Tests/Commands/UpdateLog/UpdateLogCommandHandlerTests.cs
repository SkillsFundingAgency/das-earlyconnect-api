using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.UpdateLog
{
    public class UpdateLogCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<ILogDataRepository> _mockLogDataRepository;
        public Mock<ILogger<UpdateLogCommandHandler>> _logger;
        private UpdateLogCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockLogDataRepository = new Mock<ILogDataRepository>();
            _logger = new Mock<ILogger<UpdateLogCommandHandler>>();

            _handler = new UpdateLogCommandHandler(_mockLogDataRepository.Object, _logger.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Test]
        public async Task UpdateLogData_ReturnsSuccess()
        {
            var command = new UpdateLogCommand
            {
                LogId = 1,
                Status = "UpdatedStatus",
                Error = "UpdatedError"
            };

            _mockLogDataRepository.Setup(x => x.UpdateStatusAndErrorAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.EqualTo(true));
            _mockLogDataRepository.Verify(x => x.UpdateStatusAndErrorAsync(command.LogId, command.Status, command.Error), Times.Once);
        }
    }
}
