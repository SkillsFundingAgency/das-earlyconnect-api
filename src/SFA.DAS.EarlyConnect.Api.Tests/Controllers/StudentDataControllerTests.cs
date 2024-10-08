﻿using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Api.Controllers;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests;
using SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentData;
using SFA.DAS.EarlyConnect.Api.Responses.CreateStudentOnboardData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentOnboardData;

namespace SFA.DAS.EarlyConnect.Api.Tests.Controllers
{
    [TestFixture]
    public class StudentDataControllerTests
    {
        private Fixture _fixture;
        private Mock<IMediator> _mediator;
        private StudentDataController _studentDataController;
        private Random _randomGenerator;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mediator = new Mock<IMediator>();
            _studentDataController = new StudentDataController(_mediator.Object);
            _randomGenerator = new Random();
        }

        [Test]
        public async Task POST_StudentData_Returns201()
        {
            var studentResponse = new CreateStudentDataResult { Message = "Success" };
            var studentData = CreateTestStudentData(5);
            var request = _fixture.Build<StudentDataPostRequest>()
                .With(x => x.ListOfStudentData, studentData)
                .Create();

            _mediator.Setup(x => x.Send(It.Is<CreateStudentDataCommand>(command =>
                    command.StudentDataList.First().FirstName.Equals(request.ListOfStudentData.First().FirstName)
                    && command.StudentDataList.First().LastName.Equals(request.ListOfStudentData.First().LastName)
                    && command.StudentDataList.First().DateOfBirth.Equals(request.ListOfStudentData.First().DateOfBirth)
                    && command.StudentDataList.First().Email.Equals(request.ListOfStudentData.First().Email)
                    && command.StudentDataList.First().Industry.Equals(request.ListOfStudentData.First().Industry)
                    && command.StudentDataList.First().DateInterestShown.Equals(request.ListOfStudentData.First().DateOfInterest)
                    ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentResponse);

            var actionResult = await _studentDataController.StudentData(request);

            Assert.That(actionResult, Is.InstanceOf<CreatedAtActionResult>());


            var createdResult = (CreatedAtActionResult)actionResult;
            Assert.That(201, Is.EqualTo(createdResult.StatusCode));

            var model = createdResult.Value as CreateStudentDataResponse;
            Assert.That(model, Is.Not.Null);
            Assert.That("Success", Is.EqualTo(model.Message));

            _mediator.Verify(x => x.Send(It.Is<CreateStudentDataCommand>(command =>
                    command.StudentDataList.First().FirstName.Equals(request.ListOfStudentData.First().FirstName)
                    && command.StudentDataList.First().LastName.Equals(request.ListOfStudentData.First().LastName)
                    && command.StudentDataList.First().DateOfBirth.Equals(request.ListOfStudentData.First().DateOfBirth)
                    && command.StudentDataList.First().Email.Equals(request.ListOfStudentData.First().Email)
                    && command.StudentDataList.First().Industry.Equals(request.ListOfStudentData.First().Industry)
                    && command.StudentDataList.First().DateInterestShown.Equals(request.ListOfStudentData.First().DateOfInterest)), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task POST_StudentOnboardData_Returns201()
        {
            var onboardResponse = new CreateStudentOnboardDataCommandResponse { Message = "Success" };

            var request = _fixture.Build<StudentOnboardDataPostRequest>()
                .With(x => x.Emails, _fixture.CreateMany<string>().ToList())
                .Create();


            _mediator.Setup(x => x.Send(It.Is<CreateStudentOnboardDataCommand>(command =>
                    command.Emails == request.Emails
                ), It.IsAny<CancellationToken>()))
                .ReturnsAsync(onboardResponse);

            var actionResult = await _studentDataController.StudentOnboardData(request);

            Assert.That(actionResult, Is.InstanceOf<CreatedAtActionResult>());


            var createdResult = (CreatedAtActionResult)actionResult;
            Assert.That(201, Is.EqualTo(createdResult.StatusCode));

            var model = createdResult.Value as CreateStudentOnboardDataResponse;
            Assert.That(model, Is.Not.Null);
            Assert.That("Success", Is.EqualTo(model.Message));

            _mediator.Verify(x => x.Send(It.Is<CreateStudentOnboardDataCommand>(command =>
                    command.Emails == request.Emails
                ), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private IEnumerable<StudentRequestModel> CreateTestStudentData(int numberOfStudents)
        {
            List<StudentRequestModel> studentList = new List<StudentRequestModel>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                var student = new StudentRequestModel()
                {
                    FirstName = GetRandomString(),
                    LastName = GetRandomString(),
                    DateOfBirth = GenerateRandomDateTime(),
                    Email = GetRandomString(),
                    Postcode = GetRandomString(),
                    Industry = GetRandomString(),
                    DateOfInterest = GenerateRandomDateTime()
                };
                studentList.Add(student);
            }

            return studentList;
        }

        private DateTime GenerateRandomDateTime()
        {
            DateTime start = new DateTime(1980, 11, 21);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_randomGenerator.Next(range));
        }

        public string GetRandomString()
        {
            var rand = new Random();
            return new String(Enumerable.Range(0, 20).Select(n => (Char)(rand.Next(32, 127))).ToArray());
        }
    }
}
