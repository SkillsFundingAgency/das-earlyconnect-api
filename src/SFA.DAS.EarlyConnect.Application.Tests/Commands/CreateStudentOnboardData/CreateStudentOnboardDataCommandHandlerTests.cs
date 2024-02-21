using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentOnboardData;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;
using SFA.DAS.EarlyConnect.Domain.Configuration;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateStudentOnboardData
{
    public class CreateStudentOnboardDataCommandHandlerTests
    {
        private Fixture _fixture;
        private Mock<ISurveyRepository> _surveyRepository;
        private Mock<IStudentDataRepository> _mockStudentDataRepository;
        private Mock<IStudentSurveyRepository> _studentSurveyRepository;
        private Mock<IDataProtectorService> _dataProtectorService;
        private Mock<IMessageSession> _messageSession;
        private Mock<EarlyConnectApiConfiguration> _earlyConnectApiConfiguration;
        private Mock<ILogger<CreateStudentOnboardDataCommandHandler>> _logger;
        private Mock<IMediator> _mediator;
        private CreateStudentOnboardDataCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _surveyRepository = new Mock<ISurveyRepository>();
            _mockStudentDataRepository = new Mock<IStudentDataRepository>();
            _studentSurveyRepository = new Mock<IStudentSurveyRepository>();
            _dataProtectorService = new Mock<IDataProtectorService>();
            _messageSession = new Mock<IMessageSession>();
            _logger = new Mock<ILogger<CreateStudentOnboardDataCommandHandler>>();
            _mediator = new Mock<IMediator>();
            _earlyConnectApiConfiguration = new Mock<EarlyConnectApiConfiguration>();

            _handler = new CreateStudentOnboardDataCommandHandler(
                _surveyRepository.Object,
                _mockStudentDataRepository.Object,
                _studentSurveyRepository.Object,
                _dataProtectorService.Object,
                _messageSession.Object,
                _logger.Object,
                _earlyConnectApiConfiguration.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task CreateStudentOnboardData_ReturnsCreateStudentOnboardDataCommandResponse()
        {
            var expectedResponse = new CreateMetricsDataResponse
            {
                ResultCode = ResponseCode.Success,
            };

            var command = _fixture.Create<CreateStudentOnboardDataCommand>();
            var survey = _fixture.Create<Survey>();
            var studentId = 21;
            var lepsId = 1;
            var logId = 1;
            var student = _fixture.Build<StudentData>()
                .With(x => x.Email, command.Emails[0])
                .With(x => x.Id, studentId)
                .Create();
            var studentSurvey = _fixture.Build<StudentSurvey>()
                .With(x => x.StudentId, studentId)
                .With(x => x.SurveyId, survey.Id)
                .Create();
            var authCode = "123456";
            var encryptedAuthCode = "MTIzNDU2";

            _surveyRepository.Setup(repository => repository.GetDefaultSurveyAsync())
                .ReturnsAsync(survey);
            _mockStudentDataRepository.Setup(repository => repository.GetByEmailAsync(command.Emails[0], "UCAS"))
                .ReturnsAsync(new List<StudentData> { student });
            _studentSurveyRepository.Setup(repository => repository.GetStudentSurveyByStudentIdAsync(studentId, survey.Id))
                .ReturnsAsync(new StudentSurvey { DateEmailSent = null });
            _studentSurveyRepository.Setup(repository => repository.AddStudentSurveyAsync(It.IsAny<StudentSurvey>()))
                .ReturnsAsync(studentSurvey.Id);
            _dataProtectorService.Setup(service => service.EncodedData(authCode))
                .Returns(encryptedAuthCode);
            _mediator.Setup(med => med.Send(It.IsAny<CreateLogCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(logId);

            var response = await _handler.Handle(command, CancellationToken.None);

            Assert.That(expectedResponse.ResultCode.Equals(response.ResultCode));
            Assert.That(response.ValidationErrors.IsNullOrEmpty());
            _studentSurveyRepository.Verify(x => x.UpdateStudentSurveyAsync(It.IsAny<StudentSurvey>()), Times.Once);
        }
    }
}
