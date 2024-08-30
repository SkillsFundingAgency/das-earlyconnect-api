using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers;

namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{
    public class LogControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private LogController _logDataController;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _logDataController = new LogController(_mediator.Object);
            _fixture.Customize<LogCreateRequest>(composer =>
                composer.Without(request => request.RequestIP));
        }

        [Test]
        public async Task POST_Create_Returns201()
        {
            // Arrange
            var request = _fixture.Create<LogCreateRequest>();
            var logId = 23;

            _mediator.Setup(x => x.Send(It.Is<CreateLogCommand>(command => command.Log.RequestSource == request.RequestSource
                && command.Log.RequestIP == request.RequestIP
                && command.Log.FileName == request.FileName
                && command.Log.Payload == request.Payload),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(logId);

            // Act
            var actionResult = await _logDataController.Create(request);

            Assert.That(actionResult, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = (CreatedAtActionResult)actionResult;
            Assert.That(201, Is.EqualTo(createdResult.StatusCode));

            _mediator.Verify(x => x.Send(It.Is<CreateLogCommand>(command => command.Log.RequestSource == request.RequestSource
                && command.Log.RequestIP == request.RequestIP
                && command.Log.FileName == request.FileName
                && command.Log.Payload == request.Payload),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task POST_Update_Returns200()
        {
            // Arrange
            var request = _fixture.Create<LogUpdateRequest>();
            var updateSuccessful = true;

            _mediator.Setup(x => x.Send(It.Is<UpdateLogCommand>(command => command.LogId == request.LogId
                && command.Status == request.Status
                && command.Error == request.Error),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(updateSuccessful);

            // Act
            var actionResult = await _logDataController.Update(request);
            var okResult = actionResult as OkResult;

            // Assert
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode.Equals(200));
            _mediator.Verify(x => x.Send(It.Is<UpdateLogCommand>(command => command.LogId == request.LogId
                && command.Status == request.Status
                && command.Error == request.Error),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
