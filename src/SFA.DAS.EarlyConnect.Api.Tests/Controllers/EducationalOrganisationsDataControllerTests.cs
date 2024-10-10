using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;
using SFA.DAS.EarlyConnect.Application.Models;
using System.Net;

namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{
    [TestFixture]
    public class EducationalOrganisationDataControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private EducationalOrganisationDataController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EducationalOrganisationDataController(_mediatorMock.Object);
        }

        [Test]
        public async Task EducationalOrganisationData_ReturnsOkResult_WhenMediatorReturnsData()
        {
            var lepCode = "123";
            var educationalOrganisationName = "Test School";

            var expectedResponse = new GetEducationalOrganisationsByLepCodeResult
            {
                EducationalOrganisations = new List<EducationalOrganisationsDto>
                {
                    new EducationalOrganisationsDto
                    {
                        Name = "Test School",
                        AddressLine1 = "123 Test St",
                        Town = "Test Town",
                        County = "Test County",
                        PostCode = "12345"
                    }
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetEducationalOrganisationsByLepCodeQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.EducationalOrganisationData(lepCode, educationalOrganisationName);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            var returnedData = okResult.Value as List<EducationalOrganisationsDto>;
            Assert.That(returnedData.Count, Is.EqualTo(expectedResponse.EducationalOrganisations.Count));
            Assert.That(returnedData[0].Name, Is.EqualTo("Test School"));
        }


        [Test]
        public async Task EducationalOrganisationData_ReturnsOkResult_WhenEducationalOrganisationNameIsEmpty()
        {
            var lepCode = "123";
            var educationalOrganisationName = string.Empty;

            var expectedResponse = new GetEducationalOrganisationsByLepCodeResult
            {
                EducationalOrganisations = new List<EducationalOrganisationsDto>()
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetEducationalOrganisationsByLepCodeQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.EducationalOrganisationData(lepCode, educationalOrganisationName);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            var returnedData = okResult.Value as List<EducationalOrganisationsDto>;
            Assert.That(returnedData.Count, Is.EqualTo(expectedResponse.EducationalOrganisations.Count));
        }
    }
}