using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateLog
{
    public class CreateLogCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<ILogDataRepository> _mockLogDataRepository;
        public Mock<ILogger<CreateLogCommandHandler>> _logger;
        private CreateLogCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockLogDataRepository = new Mock<ILogDataRepository>();
            _logger = new Mock<ILogger<CreateLogCommandHandler>>();

            _handler = new CreateLogCommandHandler(_mockLogDataRepository.Object, _logger.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Test]
        public async Task SavesLogData_ReturnsLogId()
        {
            var logData = new ECAPILog();
            var command = new CreateLogCommand { Log = logData };

            _mockLogDataRepository.Setup(x => x.CreateAsync(It.IsAny<ECAPILog>()))
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.AreEqual(1, result);
            _mockLogDataRepository.Verify(x => x.CreateAsync(It.IsAny<ECAPILog>()), Times.Once);
        }
    }
}
