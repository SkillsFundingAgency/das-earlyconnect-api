using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public async Task POST_MetricsData_Returns200()
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
               .ReturnsAsync(Unit.Value);

            // Act
            var actionResult = await _metricsDataController.MetricsData(request);
            var okResult = actionResult as OkResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode.Equals(200));
            _mediator.Verify(x => x.Send(It.Is<CreateMetricsDataCommand>(command =>
                   command.MetricsData.First().Region.Equals(request.MetricsData.First().Region)
                   && command.MetricsData.First().IntendedStartYear.Equals(request.MetricsData.First().IntendedStartYear)
                   && command.MetricsData.First().NoOfStudents.Equals(request.MetricsData.First().NoOfStudents)
                   && command.MetricsData.First().WillingnessToRelocate.Equals(request.MetricsData.First().WillingnessToRelocate)
                   && command.MetricsData.First().NoOfGCSCs.Equals(request.MetricsData.First().NoOfGCSCs)
                   ), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
