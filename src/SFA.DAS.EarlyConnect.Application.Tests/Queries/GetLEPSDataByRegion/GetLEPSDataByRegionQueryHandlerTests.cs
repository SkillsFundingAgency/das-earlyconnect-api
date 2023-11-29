using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSData;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsCode;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetLEPSDataByRegion
{
    public class GetLEPSDataByRegionQueryHandlerTests
    {
        public Mock<ILEPSDataRepository> _lepsDataRepository;
        private GetLEPSDataByRegionQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _lepsDataRepository = new Mock<ILEPSDataRepository>();
            _handler = new GetLEPSDataByRegionQueryHandler(_lepsDataRepository.Object);
        }

        [Test]
        public async Task RetrievesData_ReturnsLepsId()
        {
            var lepsId = 1;
            var region = "North East";
            var query = new GetLEPSDataByRegionQuery { Region = region };

            _lepsDataRepository.Setup(x => x.GetLepsIdByRegionAsync(It.Is<string>(c => c == region)))
                .ReturnsAsync(lepsId);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.That(lepsId == result);
            _lepsDataRepository.Verify(x => x.GetLepsIdByRegionAsync(It.Is<string>(c => c == region)), Times.Once);
        }
    }
}
