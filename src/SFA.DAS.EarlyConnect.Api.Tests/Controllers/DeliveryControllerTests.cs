using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.DeliveryUpdate;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Application.Responses;


namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{
    public class DeliveryControllerControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private DeliveryController _deliveryController;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _deliveryController = new DeliveryController(_mediator.Object);
        }

        [Test]
        public async Task POST_Delivery_Returns200()
        {
            // Arrange
            var request = _fixture.Create<DeliveryUpdateRequest>();
            var result = _fixture.Create<DeliveryUpdateResult>();

            _mediator.Setup(x => x.Send(It.Is<DeliveryUpdateCommand>(command => command.Source == request.Source && command.Ids == request.Ids),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var actionResult = await _deliveryController.Update(request);
            var okResult = actionResult as OkObjectResult;

            // Assert
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode.Equals(200));
            _mediator.Verify(x => x.Send(It.Is<DeliveryUpdateCommand>(command => command.Source == request.Source
                && command.Ids == request.Ids),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task POST_Delivery_Returns400()
        {
            // Arrange
            var request = _fixture.Create<DeliveryUpdateRequest>();
            var result = _fixture.Create<DeliveryUpdateResult>();

            _mediator.Setup(x => x.Send(It.Is<DeliveryUpdateCommand>(command => command.Source == request.Source && command.Ids == request.Ids),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeliveryUpdateResult
                {
                    ResultCode = ResponseCode.InvalidRequest,
                    ValidationErrors = new List<DetailedValidationError>
                    {
                        new DetailedValidationError
                        {
                            Field = "Ids", Message = "Invalid Ids in File " + string.Join(",", new List<int>{ 1,2,3})
                        }
                    }.Cast<object>().ToList()
                });

            // Act
            var actionResult = await _deliveryController.Update(request);
            var badRequestResult = actionResult as BadRequestObjectResult;

            // Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode.Equals(400));
        }


    }
}
