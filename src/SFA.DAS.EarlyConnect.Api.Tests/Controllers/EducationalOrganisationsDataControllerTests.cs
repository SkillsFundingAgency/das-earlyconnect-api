using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;
using SFA.DAS.EarlyConnect.Application.Models;
using System.Net;
using SFA.DAS.EarlyConnect.Api.Requests.GetRequests;
using SFA.DAS.EarlyConnect.Api.Responses.GetEducationalOrganisationsByLepCode;

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
            var request = new EducationalOrganisationsGetRequest
            {
                LepCode = "123",
                SearchTerm = "Test School"
            };

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
                .Setup(m => m.Send(It.IsAny<GetEducationalOrganisationsByLepCodeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.EducationalOrganisationsData(request);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            var returnedData = okResult.Value as GetEducationalOrganisationsResponse;
            Assert.That(returnedData.EducationalOrganisations, Has.Count.EqualTo(expectedResponse.EducationalOrganisations.Count));
            var firstOrganisation = returnedData.EducationalOrganisations.ElementAt(0);
            Assert.That(firstOrganisation.Name, Is.EqualTo("Test School"));
        }

        [Test]
        public async Task EducationalOrganisationData_ReturnsOkResult_WhenSearchTermIsEmpty()
        {
            var request = new EducationalOrganisationsGetRequest
            {
                LepCode = "123",
                SearchTerm = string.Empty
            };

            var expectedResponse = new GetEducationalOrganisationsByLepCodeResult
            {
                EducationalOrganisations = new List<EducationalOrganisationsDto>()
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetEducationalOrganisationsByLepCodeQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.EducationalOrganisationsData(request);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            var returnedData = okResult.Value as GetEducationalOrganisationsResponse;
            Assert.That(returnedData.EducationalOrganisations, Has.Count.EqualTo(expectedResponse.EducationalOrganisations.Count));
        }
    }
}