using AutoFixture;
using Azure.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Application.Services.AuthCodeService;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;
using SFA.DAS.EarlyConnect.Data.Repository;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateOtherStudentTriageData
{
    public class CreateOtherStudentTriageDataCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<ISurveyRepository> _surveyRepository;
        public Mock<IStudentDataRepository> _mockStudentDataRepository;
        public Mock<ILEPSDataRepository> _lepsDataRepository;
        public Mock<IStudentSurveyRepository> _studentSurveyRepository;
        public Mock<IDataProtectorService> _dataProtectorService;
        public Mock<IAuthCodeService> _authCodeService;
        public Mock<IMessageSession> _messageSession;
        public Mock<ILogger<CreateOtherStudentTriageDataCommandHandler>> _logger;
        public Mock<IMediator> _mediator;
        private CreateOtherStudentTriageDataCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _surveyRepository = new Mock<ISurveyRepository>();
            _mockStudentDataRepository = new Mock<IStudentDataRepository>();
            _lepsDataRepository = new Mock<ILEPSDataRepository>();
            _studentSurveyRepository = new Mock<IStudentSurveyRepository>();
            _dataProtectorService = new Mock<IDataProtectorService>();
            _authCodeService = new Mock<IAuthCodeService>();
            _messageSession = new Mock<IMessageSession>();
            _logger = new Mock<ILogger<CreateOtherStudentTriageDataCommandHandler>>();
            _mediator = new Mock<IMediator>();
            _handler = new CreateOtherStudentTriageDataCommandHandler(
                _surveyRepository.Object,
                _mockStudentDataRepository.Object,
                _lepsDataRepository.Object,
                _studentSurveyRepository.Object,
                _dataProtectorService.Object,
                _authCodeService.Object,
                _messageSession.Object,
                _logger.Object,
                _mediator.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task CreatesStudentDataAndStudentSurvey_ReturnsCreateOtherStudentTriageDataCommandResponse()
        {

            // Arrange
            var command = _fixture.Create<CreateOtherStudentTriageDataCommand>();
            var survey = _fixture.Create<Survey>();
            var studentId = 21;
            var lepsId = 1;
            var logId = 1;
            var student = _fixture.Build<StudentData>()
                .With(x => x.Email, command.Email)
                .With(x => x.Id, studentId)
                .Create();
            var studentData = _fixture.Build<StudentData>()
                .With(x => x.Email, command.Email)
                .With(x => x.LepsId, lepsId)
                .Create();
            var studentSurvey = _fixture.Build<StudentSurvey>()
                .With(x => x.StudentId, studentId)
                .With(x => x.SurveyId, survey.Id)
                .Create();
            var authCode = "123456";
            var encryptedAuthCode = "MTIzNDU2";

            _surveyRepository.Setup(repository => repository.GetDefaultSurveyAsync())
                .ReturnsAsync(survey);
            _lepsDataRepository.Setup(repository => repository.GetLepsIdByLepsCodeAsync(command.LepsCode))
                .ReturnsAsync(lepsId);
            _mockStudentDataRepository.Setup(repository => repository.GetByEmailAsync(command.Email, "Other"))
                .ReturnsAsync(student);
            _mockStudentDataRepository.Setup(repository => repository.AddStudentDataAsync(new StudentData { Email = command.Email, LepsId = lepsId }))
                .ReturnsAsync(studentId);
            _studentSurveyRepository.Setup(repository => repository.AddStudentSurveyAsync(It.IsAny<StudentSurvey>()))
                .ReturnsAsync(studentSurvey.Id);
            _authCodeService.Setup(service => service.Generate6DigitCode())
                .Returns(authCode);
            _dataProtectorService.Setup(service => service.EncodedData(authCode))
                .Returns(encryptedAuthCode);
            _mediator.Setup(med => med.Send(It.IsAny<CreateLogCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(logId);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.StudentSurveyId.Equals(studentSurvey.Id.ToString()));
            Assert.That(response.AuthCode.Equals(encryptedAuthCode));
        }
    }
}
