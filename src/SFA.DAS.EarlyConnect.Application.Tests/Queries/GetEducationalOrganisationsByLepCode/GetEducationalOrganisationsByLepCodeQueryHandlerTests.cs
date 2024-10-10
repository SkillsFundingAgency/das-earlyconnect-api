using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Queries.GetEducationalOrganisationsByLepCode;
using SFA.DAS.EarlyConnect.Domain.Dtos;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Tests.Queries.GetEducationalOrganisationsByLepCode
{
    [TestFixture]
    public class GetEducationalOrganisationsByLepCodeQueryHandlerTests
    {
        private Mock<IEducationalOrganisationRepository> _educationalOrganisationRepositoryMock;
        private GetEducationalOrganisationsByLepCodeQueryHandler _sut;

        [SetUp]
        public void Setup()
        {
            _educationalOrganisationRepositoryMock = new Mock<IEducationalOrganisationRepository>();
            _sut = new GetEducationalOrganisationsByLepCodeQueryHandler(_educationalOrganisationRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnMappedEducationalOrganisations_WhenDataExists()
        {
            var request = new GetEducationalOrganisationsByLepCodeQuery
            { LepCode = "LEP001", EducationalOrganisationName = "School" };
            var educationalOrganisations = new List<EducationalOrganisation>
            {
                new EducationalOrganisation
                {
                    Name = "School A", AddressLine1 = "Street A", Town = "Town A", County = "County A",
                    PostCode = "12345"
                }
            };

            _educationalOrganisationRepositoryMock
                .Setup(repo => repo.GetNameByLepCodeAsync(request.LepCode, request.EducationalOrganisationName))
                .ReturnsAsync(educationalOrganisations);

            var result = await _sut.Handle(request, CancellationToken.None);

            Assert.That(result.EducationalOrganisations, Has.Count.EqualTo(1));

            var organisationA = result.EducationalOrganisations.First();
            Assert.That(organisationA.Name, Is.EqualTo("School A"));
            Assert.That(organisationA.AddressLine1, Is.EqualTo("Street A"));
            Assert.That(organisationA.Town, Is.EqualTo("Town A"));
            Assert.That(organisationA.County, Is.EqualTo("County A"));
            Assert.That(organisationA.PostCode, Is.EqualTo("12345"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoEducationalOrganisationsExist()
        {
            var request = new GetEducationalOrganisationsByLepCodeQuery
            { LepCode = "LEP002", EducationalOrganisationName = "NonExistingSchool" };
            _educationalOrganisationRepositoryMock
                .Setup(repo => repo.GetNameByLepCodeAsync(request.LepCode, request.EducationalOrganisationName))
                .ReturnsAsync(new List<EducationalOrganisation>());

            var result = await _sut.Handle(request, CancellationToken.None);

            Assert.That(result.EducationalOrganisations, Is.Empty);
        }

        [Test]
        public void Handle_ShouldThrowException_WhenRepositoryThrowsException()
        {
            var request = new GetEducationalOrganisationsByLepCodeQuery
            { LepCode = "LEP003", EducationalOrganisationName = "School" };
            _educationalOrganisationRepositoryMock
                .Setup(repo => repo.GetNameByLepCodeAsync(request.LepCode, request.EducationalOrganisationName))
                .ThrowsAsync(new Exception("Repository error"));

            Assert.That(async () => await _sut.Handle(request, CancellationToken.None),
                Throws.Exception.TypeOf<Exception>());
        }

        [Test]
        public async Task Handle_ShouldReturnAllMatchingLepCodeData_WhenNameIsNullOrEmpty()
        {
            var request = new GetEducationalOrganisationsByLepCodeQuery
            { LepCode = "LEP001", EducationalOrganisationName = null };
            var educationalOrganisations = new List<EducationalOrganisation>
            {
                new EducationalOrganisation
                {
                    Name = "School A", AddressLine1 = "Street A", Town = "Town A", County = "County A",
                    PostCode = "12345"
                }
            };

            _educationalOrganisationRepositoryMock
                .Setup(repo => repo.GetNameByLepCodeAsync(request.LepCode, request.EducationalOrganisationName))
                .ReturnsAsync(educationalOrganisations);

            var result = await _sut.Handle(request, CancellationToken.None);

            Assert.That(result.EducationalOrganisations, Has.Count.EqualTo(1));

            var organisation = result.EducationalOrganisations.First();
            ;
            Assert.That(organisation.Name, Is.EqualTo("School A"));
            Assert.That(organisation.AddressLine1, Is.EqualTo("Street A"));
            Assert.That(organisation.Town, Is.EqualTo("Town A"));
            Assert.That(organisation.County, Is.EqualTo("County A"));
            Assert.That(organisation.PostCode, Is.EqualTo("12345"));
        }
    }
}