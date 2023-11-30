using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers;

namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{
    [TestFixture]
    public class LepsDataControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private LepsDataController _lepsDataController;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _lepsDataController = new LepsDataController(_mediator.Object);
        }

        [Test]
        public async Task GET_LepsDataWithUsers_ReturnsLepsDataWithUsers()
        {
            // Arrange
            var request = _fixture.Create<GetLEPSDataWithUsersQuery>();
            var lepsDataTestDto = _fixture.Create<ICollection<LEPSDataDto>>();
            var expectedResult = _fixture.Build<GetLEPDataWithUsersResult>()
                                .With(x => x.LEPSData, lepsDataTestDto)
                                .Create();

            _mediator.Setup(x => x.Send(It.IsAny<GetLEPSDataWithUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var actionResult = await _lepsDataController.LepsDataWithUsers();
            var okObjectResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okObjectResult);
            Assert.That(okObjectResult.StatusCode.Equals(200));
        }
    }
}
