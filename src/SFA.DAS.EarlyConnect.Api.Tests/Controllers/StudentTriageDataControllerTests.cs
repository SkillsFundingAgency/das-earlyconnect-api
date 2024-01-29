using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Queries.GetStudentTriageDataBySurveyId;

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

            _mediator.Setup(x => x.Send(It.Is<CreateOtherStudentTriageDataCommand>(command => command.Email == request.Email 
            && command.LepsCode == request.LepsCode), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            // Act
            var actionResult = await _studentTriageDataController.StudentTriageDataOther(request);
            var createdAtActionResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(createdAtActionResult);
            Assert.That(createdAtActionResult.StatusCode.Equals(201));
        }

        [Test]
        public async Task POST_StudentTriageData_ReturnsCreatedAt()
        {
            // Arrange
            var request = _fixture.Create<StudentTriageDataPostRequest>();
            var studentSurveyGuid = Guid.NewGuid();
            var expectedResponse = new CreateStudentDataResponse { Message = "Success" };
            var expectedResult = new CreateStudentTriageDataResult { Message = "Success" };

            _mediator.Setup(x => x.Send(It.Is<CreateStudentTriageDataCommand>(command => command.StudentSurveyGuid == studentSurveyGuid
            && command.StudentData.FirstName == request.FirstName), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actionResult = await _studentTriageDataController.StudentTriageData(request, studentSurveyGuid);
            var createdAtActionResult = actionResult as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(createdAtActionResult);
            Assert.That(createdAtActionResult.StatusCode.Equals(201));
        }

        [Test]
        public async Task POST_StudentTriageData_WrongStudentId_BadRequest()
        {
            // Arrange
            var request = _fixture.Create<StudentTriageDataPostRequest>();
            var studentSurveyGuid = Guid.NewGuid();
            var expectedResponse = new CreateStudentDataResponse { Message = "No Student ID Found!" };
            var expectedResult = new CreateStudentTriageDataResult { Message = "No Student ID Found!" };

            _mediator.Setup(x => x.Send(It.Is<CreateStudentTriageDataCommand>(command => command.StudentSurveyGuid == studentSurveyGuid
            && command.StudentData.FirstName == request.FirstName), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actionResult = await _studentTriageDataController.StudentTriageData(request, studentSurveyGuid);
            var badRequestObjectResult = actionResult as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestObjectResult);
            Assert.That(badRequestObjectResult.StatusCode.Equals(400));
        }

        [Test]
        public async Task POST_StudentTriageData_WrongStudentSurveyId_BadRequest()
        {
            // Arrange
            var request = _fixture.Create<StudentTriageDataPostRequest>();
            var studentSurveyGuid = Guid.NewGuid();
            var expectedResponse = new CreateStudentDataResponse { Message = "Cannot find student answer by the supplied ID" };
            var expectedResult = new CreateStudentTriageDataResult { Message = "Cannot find student answer by the supplied ID" };

            _mediator.Setup(x => x.Send(It.Is<CreateStudentTriageDataCommand>(command => command.StudentSurveyGuid == studentSurveyGuid
            && command.StudentData.FirstName == request.FirstName), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));

            // Act
            var actionResult = await _studentTriageDataController.StudentTriageData(request, studentSurveyGuid);
            var badRequestObjectResult = actionResult as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(badRequestObjectResult);
            Assert.That(badRequestObjectResult.StatusCode.Equals(400));
        }

        [Test]
        public async Task GET_StudentTriageData_ReturnsStudentTriageData()
        {
            string surveyGuid = new Guid().ToString();
            var studentTriageDataDto = _fixture.Create<StudentTriageDataDto>();
            var expectedResult = _fixture.Build<GetStudentTriageDataBySurveyIdResult>()
                .With(x => x.StudentTriageData, studentTriageDataDto)
                .Create();

            _mediator.Setup(x => x.Send(It.IsAny<GetStudentTriageDataBySurveyIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var actionResult = await _studentTriageDataController.StudentTriageData(surveyGuid);
            var okObjectResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okObjectResult);
            Assert.That(okObjectResult.StatusCode.Equals(200));
        }
    }
}