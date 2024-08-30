using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Responses;


namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{
    public class MetricsDataControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private MetricsDataController _metricsDataController;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _metricsDataController = new MetricsDataController(_mediator.Object);
        }

        [Test]
        public async Task POST_MetricsData_Returns201()
        {
            // Arrange
            var request = _fixture.Create<MetricsDataPostRequest>();

            _mediator.Setup(x => x.Send(It.Is<CreateMetricsDataCommand>(command =>
                   command.MetricsData.First().Region.Equals(request.MetricsData.First().Region)
                   && command.MetricsData.First().IntendedStartYear.Equals(request.MetricsData.First().IntendedStartYear)
                   && command.MetricsData.First().NoOfStudents.Equals(request.MetricsData.First().NoOfStudents)
                   && command.MetricsData.First().WillingnessToRelocate.Equals(request.MetricsData.First().WillingnessToRelocate)
                   && command.MetricsData.First().NoOfGCSCs.Equals(request.MetricsData.First().NoOfGCSCs)
                   ), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new CreateMetricsDataResponse { ResultCode = Application.Responses.ResponseCode.Success });

            // Act
            var actionResult = await _metricsDataController.MetricsData(request);

            Assert.That(actionResult, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = (CreatedAtActionResult)actionResult;
            Assert.That(201, Is.EqualTo(createdResult.StatusCode));


            _mediator.Verify(x => x.Send(It.Is<CreateMetricsDataCommand>(command =>
                   command.MetricsData.First().Region.Equals(request.MetricsData.First().Region)
                   && command.MetricsData.First().IntendedStartYear.Equals(request.MetricsData.First().IntendedStartYear)
                   && command.MetricsData.First().NoOfStudents.Equals(request.MetricsData.First().NoOfStudents)
                   && command.MetricsData.First().WillingnessToRelocate.Equals(request.MetricsData.First().WillingnessToRelocate)
                   && command.MetricsData.First().NoOfGCSCs.Equals(request.MetricsData.First().NoOfGCSCs)
                   ), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task POST_MetricsData_InvalidLepsCode_Returns400()
        {
            // Arrange
            var request = _fixture.Create<MetricsDataPostRequest>();

            _mediator.Setup(x => x.Send(It.Is<CreateMetricsDataCommand>(command =>
                   command.MetricsData.First().Region.Equals(request.MetricsData.First().Region)
                   && command.MetricsData.First().IntendedStartYear.Equals(request.MetricsData.First().IntendedStartYear)
                   && command.MetricsData.First().NoOfStudents.Equals(request.MetricsData.First().NoOfStudents)
                   && command.MetricsData.First().WillingnessToRelocate.Equals(request.MetricsData.First().WillingnessToRelocate)
                   && command.MetricsData.First().NoOfGCSCs.Equals(request.MetricsData.First().NoOfGCSCs)
                   ), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new CreateMetricsDataResponse
               {
                   ResultCode = ResponseCode.InvalidRequest,
                   ValidationErrors = new List<DetailedValidationError>
                                {
                                    new DetailedValidationError
                                    {
                                        Field = "LepCode", Message = "Cannot get LEPSData by the provided LepCode"
                                    }
                                }.Cast<object>().ToList()
               });

            // Act
            var actionResult = await _metricsDataController.MetricsData(request);
            var badRequestResult = actionResult as BadRequestObjectResult;

            // Assert
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(badRequestResult, Is.Not.Null);
            Assert.That(badRequestResult.StatusCode.Equals(400));
        }
    }
}
