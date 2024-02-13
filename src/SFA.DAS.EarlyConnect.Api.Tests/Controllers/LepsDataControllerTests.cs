using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentFeedback;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentFeedbackCommand;
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

        [Test]
        public async Task POST_Create_Returns201()
        {
            var studentFeedbackResponse = new CreateStudentFeedbackResult { Message = "Success" };
            var studentFeedbackData = CreateTestStudentFeedback(5);
            var request = _fixture.Build<StudentFeedbackPostRequest>()
                .With(x => x.ListOfStudentFeedback, studentFeedbackData)
                .Create();

            _mediator.Setup(x => x.Send(It.Is<CreateStudentFeedbackCommand>(command =>
                    command.StudentFeedbackList.First().StatusUpdate.Equals(request.ListOfStudentFeedback.First().StatusUpdate)
                    && command.StudentFeedbackList.First().UpdatedBy.Equals(request.ListOfStudentFeedback.First().UpdatedBy)
                    && command.StudentFeedbackList.First().Notes.Equals(request.ListOfStudentFeedback.First().Notes)
                    ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentFeedbackResponse);

            var actionResult = await _lepsDataController.StudentFeedback(request);

            Assert.IsInstanceOf<CreatedAtActionResult>(actionResult);

            var createdResult = (CreatedAtActionResult)actionResult;
            Assert.AreEqual(201, createdResult.StatusCode);

            var model = createdResult.Value as CreateStudentFeedbackResponse;
            Assert.IsNotNull(model);
            Assert.AreEqual("Success", model.Message);

            _mediator.Verify(x => x.Send(It.Is<CreateStudentFeedbackCommand>(command =>
                    command.StudentFeedbackList.First().StatusUpdate.Equals(request.ListOfStudentFeedback.First().StatusUpdate)
                    && command.StudentFeedbackList.First().UpdatedBy.Equals(request.ListOfStudentFeedback.First().UpdatedBy)
                    && command.StudentFeedbackList.First().Notes.Equals(request.ListOfStudentFeedback.First().Notes)), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private IEnumerable<StudentFeedbackRequestModel> CreateTestStudentFeedback(int numberOfStudentFeedbacks)
        {
            List<StudentFeedbackRequestModel> studentFeedbackList = new List<StudentFeedbackRequestModel>();

            for (int i = 0; i < numberOfStudentFeedbacks; i++)
            {
                var feedback = new StudentFeedbackRequestModel()
                {
                    Notes = GetRandomString(),
                    StatusUpdate = GetRandomString(),
                    UpdatedBy = GetRandomString()
                };

                studentFeedbackList.Add(feedback);
            }

            return studentFeedbackList;
        }

        public string GetRandomString()
        {
            var rand = new Random();
            return new String(Enumerable.Range(0, 20).Select(n => (Char)(rand.Next(32, 127))).ToArray());
        }
    }
}
