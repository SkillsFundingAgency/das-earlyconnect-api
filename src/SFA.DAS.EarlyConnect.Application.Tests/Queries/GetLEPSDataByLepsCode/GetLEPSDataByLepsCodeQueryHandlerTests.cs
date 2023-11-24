using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsCode;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetLEPSDataByLepsCode
{
    public class GetLEPSDataByLepsCodeQueryHandlerTests
    {
        public Mock<ILEPSDataRepository> _lepsDataRepository;
        private GetLEPSDataByLepsCodeQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _lepsDataRepository = new Mock<ILEPSDataRepository>();
            _handler = new GetLEPSDataByLepsCodeQueryHandler(_lepsDataRepository.Object);
        }

        [Test]
        public async Task RetrievesData_ReturnsLepsDataDto()
        {
            var lepsCode = "E0001919";
            var lepsId = 1;
            var region = "North East";
            var query = new GetLEPSDataByLepsCodeQuery { LEPSCode = lepsCode };

            _lepsDataRepository.Setup(x => x.GetLepsIdByLepsCodeAsync(It.Is<string>(c => c == lepsCode)))
                .ReturnsAsync(lepsId);
            _lepsDataRepository.Setup(x => x.GetLepsRegionByLepsCodeAsync(It.Is<string>(c => c == lepsCode)))
                .ReturnsAsync(region);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(lepsId == result.LepId);
            Assert.That(region == result.Region);
            _lepsDataRepository.Verify(x => x.GetLepsIdByLepsCodeAsync(It.Is<string>(c => c == lepsCode)), Times.Once);
            _lepsDataRepository.Verify(x => x.GetLepsRegionByLepsCodeAsync(It.Is<string>(c => c == lepsCode)), Times.Once);
        }
    }
}
