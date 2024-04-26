using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateOtherStudentTriageData
{
    public class SendReminderEmailCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<IStudentDataRepository> _mockStudentDataRepository;
        public Mock<IStudentSurveyRepository> _studentSurveyRepository;
        public Mock<IMessageSession> _messageSession;
        public Mock<ILogger<SendReminderEmailCommandHandler>> _logger;
        private SendReminderEmailCommandHandler _handler;
        private Mock<EarlyConnectApiConfiguration> _earlyConnectApiConfiguration;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockStudentDataRepository = new Mock<IStudentDataRepository>();
            _studentSurveyRepository = new Mock<IStudentSurveyRepository>();
            _messageSession = new Mock<IMessageSession>();
            _logger = new Mock<ILogger<SendReminderEmailCommandHandler>>();
            _earlyConnectApiConfiguration = new Mock<EarlyConnectApiConfiguration>();
            _handler = new SendReminderEmailCommandHandler(
                _mockStudentDataRepository.Object,
                _logger.Object,
                _messageSession.Object,
                _studentSurveyRepository.Object,
                _earlyConnectApiConfiguration.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task SendReminderEmail_ReturnsSuccessResponse()
        {
            var command = _fixture.Create<SendReminderEmailCommand>();
            var studentData = _fixture.Create<List<StudentData>>();

            _mockStudentDataRepository.Setup(repo => repo.GetEmailByLepcodeAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(studentData);

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Message, Is.EqualTo($"Reminder email sent to {studentData.Count} students"));
        }
    }
}
