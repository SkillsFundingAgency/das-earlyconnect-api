using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSUserByLepsId;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetLEPSDataWithUsers
{
    [TestFixture]
    public class GetLEPSDataWithUsersQueryHandlerTests
    {
        private Fixture _fixture;
        public Mock<ILEPSDataRepository> _lepsDataRepository;
        public Mock<IMediator> _mediator;
        private GetLEPSDataWithUsersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _lepsDataRepository = new Mock<ILEPSDataRepository>();
            _mediator = new Mock<IMediator>();
            _handler = new GetLEPSDataWithUsersQueryHandler(_lepsDataRepository.Object, _mediator.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public async Task RetrievesLEPSData_ReturnsLepsDataWithUsers()
        {
            var query = new GetLEPSDataWithUsersQuery { };
            var lepsDataList = _fixture.Create<ICollection<LEPSData>>();

            _lepsDataRepository.Setup(x => x.GetAllLepsDataAsync())
                .ReturnsAsync(lepsDataList);
            _mediator.Setup(x => x.Send(It.IsAny<GetLEPSUserByLepsIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_fixture.Create<GetLEPSUsersByLepsIdResult>());

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.IsInstanceOf<GetLEPDataWithUsersResult>(result);
            _lepsDataRepository.Verify(x => x.GetAllLepsDataAsync(), Times.Once);
            _mediator.Verify(x => x.Send(It.IsAny<GetLEPSUserByLepsIdQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(lepsDataList.Count()));
            Assert.That(lepsDataList.Count().Equals(result.LEPSData.Count()));
            Assert.That(lepsDataList.First().LepName.Equals(result.LEPSData.First().LepName));
        }
    }
}
