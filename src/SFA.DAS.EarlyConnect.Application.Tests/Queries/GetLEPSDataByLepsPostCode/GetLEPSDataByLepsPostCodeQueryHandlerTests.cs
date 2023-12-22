using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsPostCode;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetLEPSDataByLepsPostCode
{
    public class GetLEPSDataByLepsPostCodeQueryHandlerTests
    {
        public Mock<ILEPSDataRepository> _lepsDataRepository;
        private GetLEPSDataByLepsPostCodeQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _lepsDataRepository = new Mock<ILEPSDataRepository>();
            _handler = new GetLEPSDataByLepsPostCodeQueryHandler(_lepsDataRepository.Object);
        }

        [Test]
        public async Task RetrievesData_ReturnsLepsId()
        {
            var postCode = "E0001919";
            var lepsId = 1;

            var query = new GetLEPSDataByLepsPostCodeQuery { PostCode = postCode };

            _lepsDataRepository.Setup(x => x.GetLepsIdByPostCodeAsync(It.Is<string>(c => c == postCode)))
                .ReturnsAsync(lepsId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(lepsId == result);
            _lepsDataRepository.Verify(x => x.GetLepsIdByPostCodeAsync(It.Is<string>(c => c == postCode)), Times.Once);
        }
    }
}
