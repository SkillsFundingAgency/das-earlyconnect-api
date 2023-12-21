using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task GET_LepsDataWithUsers_ReturnsLepsDataWithUsers()
        {
            // Arrange
            var request = _fixture.Create<StudentTriageDataOtherPostRequest>();
            var expectedResult = _fixture.Create<CreateOtherStudentTriageDataCommandResponse>();

            _mediator.Setup(x => x.Send(It.Is<StudentTriageDataOtherPostRequest>(command => command.Email == request.Email 
            && command.LepsCode == request.LepsCode), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var actionResult = await _studentTriageDataController.StudentTriageData(request);
            var okObjectResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okObjectResult);
            Assert.That(okObjectResult.StatusCode.Equals(200));
        }
    }
}