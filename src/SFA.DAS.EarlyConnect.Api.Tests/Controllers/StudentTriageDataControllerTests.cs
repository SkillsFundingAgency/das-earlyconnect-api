using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;

namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{

    [TestFixture]
    public class StudentTriageDataControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private StudentTriageDataController _studentTriageDataController;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _studentTriageDataController = new StudentTriageDataController(_mediator.Object);
        }

        [Test]
        public async Task POST_StudentTriageDataOther_ReturnsOk()
        {
            // Arrange
            var request = _fixture.Create<StudentTriageDataOtherPostRequest>();
            var expectedResult = _fixture.Create<CreateOtherStudentTriageDataCommandResponse>();

            _mediator.Setup(x => x.Send(It.Is<StudentTriageDataOtherPostRequest>(command => command.Email == request.Email 
            && command.LepsCode == request.LepsCode), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var actionResult = await _studentTriageDataController.StudentTriageDataOther(request);
            var okObjectResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okObjectResult);
            Assert.That(okObjectResult.StatusCode.Equals(200));
        }
    }
}