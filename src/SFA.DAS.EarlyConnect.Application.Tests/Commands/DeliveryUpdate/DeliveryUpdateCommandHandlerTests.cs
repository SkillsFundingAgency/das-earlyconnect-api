using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.DeliveryUpdate;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.DeliveryUpdate
{
    public class DeliveryUpdateCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<IStudentDataRepository> _studentDataRepository;
        public Mock<IMetricsDataRepository> _metricsDataRepository;
        public Mock<ISchoolsLeadsDataRepository> _schoolsLeadsDataRepository;
        public Mock<ISubjectPreferenceDataRepository> _subjectPreferenceDataRepository;
        public Mock<ILogger<DeliveryUpdateCommandHandler>> _logger;
        public Mock<IMediator> _mediator;
        private DeliveryUpdateCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _studentDataRepository = new Mock<IStudentDataRepository>();
            _metricsDataRepository = new Mock<IMetricsDataRepository>();
            _schoolsLeadsDataRepository = new Mock<ISchoolsLeadsDataRepository> ();
            _subjectPreferenceDataRepository = new Mock<ISubjectPreferenceDataRepository>();
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<DeliveryUpdateCommandHandler>>();

            _handler = new DeliveryUpdateCommandHandler(
                _studentDataRepository.Object, 
                _metricsDataRepository.Object, 
                _schoolsLeadsDataRepository.Object, 
                _subjectPreferenceDataRepository.Object, 
                _logger.Object,
                _mediator.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Test]
        public async Task Update_StudentData_ReturnsSuccess()
        {
            var command = new DeliveryUpdateCommand
            {
                Source = "StudentData",
                Ids = new List<int> { 1, 2},
            };

            _studentDataRepository.Setup(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()))
                .Returns(Task.FromResult(new List<int>()));

            var result = await _handler.Handle(command, CancellationToken.None);

            _studentDataRepository.Verify(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()), Times.Once);
        }

        [Test]
        public async Task Update_ApprenticeMetricsData_ReturnsSuccess()
        {
            var command = new DeliveryUpdateCommand
            {
                Source = "ApprenticeMetricsData",
                Ids = new List<int> { 1, 2 },
            };

            _metricsDataRepository.Setup(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()))
                .Returns(Task.FromResult(new List<int>()));

            var result = await _handler.Handle(command, CancellationToken.None);

            _metricsDataRepository.Verify(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()), Times.Once);
        }

        [Test]
        public async Task Update_SchoolsLeadsData_ReturnsSuccess()
        {
            var command = new DeliveryUpdateCommand
            {
                Source = "SchoolsLeadsData",
                Ids = new List<int> { 1, 2 },
            };

            _schoolsLeadsDataRepository.Setup(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()))
                .Returns(Task.FromResult(new List<int>()));

            var result = await _handler.Handle(command, CancellationToken.None);

            _schoolsLeadsDataRepository.Verify(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()), Times.Once);
        }

        [Test]
        public async Task Update_SubjectPreferenceData_ReturnsSuccess()
        {
            var command = new DeliveryUpdateCommand
            {
                Source = "SubjectPreferenceData",
                Ids = new List<int> { 1, 2 },
            };

            _subjectPreferenceDataRepository.Setup(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()))
                .Returns(Task.FromResult(new List<int>()));

            var result = await _handler.Handle(command, CancellationToken.None);
            _subjectPreferenceDataRepository.Verify(x => x.UpdateLepsDateSent(It.IsAny<List<int>>()), Times.Once);
        }
    }
}
