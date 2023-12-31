﻿using AutoFixture;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using SFA.DAS.EarlyConnect.Application.Commands.CreateMetricsData;
using SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByRegion;
using SFA.DAS.EarlyConnect.Application.Queries.GetMetricsFlag;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.EarlyConnect.Application.Responses;

namespace SFA.DAS.EarlyConnect.Application.Tests.Commands.CreateMetricsData
{
    public class CreateMetricsDataCommandHandlerTests
    {
        private Fixture _fixture;
        public Mock<IMetricsDataRepository> _mockMetricsDataRepository;
        public Mock<ILogger<CreateMetricsDataCommandHandler>> _logger;
        private CreateMetricsDataCommandHandler _handler;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockMetricsDataRepository = new Mock<IMetricsDataRepository>();
            _logger = new Mock<ILogger<CreateMetricsDataCommandHandler>>();
            _mediatorMock = new Mock<IMediator>();

            _handler = new CreateMetricsDataCommandHandler(_mockMetricsDataRepository.Object, _mediatorMock.Object, _logger.Object);

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        }

        [Test]
        public async Task SavesMetricsData_ReturnsSuccess()
        {
            var expectedResponse = new CreateMetricsDataResponse 
            {
                ResultCode = ResponseCode.Success,
            };

            var command = new CreateMetricsDataCommand
            {
                MetricsData = new List<MetricDto>
                {
                    new MetricDto
                    {
                        Region = "TestRegion",
                        IntendedStartYear = 2023,
                        MaxTravelInMiles = 50,
                        WillingnessToRelocate = true,
                        NoOfGCSCs = 3,
                        NoOfStudents = 100,
                        LogId = 1,
                        MetricFlags = new List<string>
                        {
                            "FlagA",
                            "FlagB",
                        }
                    }
                }
            };

            var metricsFlags = new List<MetricsFlag>
            {
                new MetricsFlag
                {
                    Id = 1,
                    FlagName = "FlagA",
                    FlagCode = "FlagA",
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    MetricsFlagLookups = new List<ApprenticeMetricsFlagData>()
                },
                new MetricsFlag
                {
                    Id = 2,
                    FlagName = "FlagB",
                    FlagCode = "FlagB",
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    MetricsFlagLookups = new List<ApprenticeMetricsFlagData>()
                },
        };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetMetricsFlagQuery>(), new CancellationToken()))
                .ReturnsAsync(metricsFlags);

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLEPSDataByRegionQuery>(), new CancellationToken()))
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(expectedResponse.ResultCode.Equals(result.ResultCode));
            Assert.That(result.ValidationErrors.IsNullOrEmpty());
            _mockMetricsDataRepository.Verify(x => x.AddManyAndDelete(It.IsAny<List<ApprenticeMetricsData>>()), Times.Once);
        }

        [Test]
        public async Task NoMetricsFlag_ReturnsNoMetricsFlagError()
        {
            var expectedResponse = new CreateMetricsDataResponse
            {
                ResultCode = Responses.ResponseCode.InvalidRequest,
            };

            var command = new CreateMetricsDataCommand
            {
                MetricsData = new List<MetricDto>
                {
                    new MetricDto
                    {
                        Region = "TestRegion",
                        IntendedStartYear = 2023,
                        MaxTravelInMiles = 50,
                        WillingnessToRelocate = true,
                        NoOfGCSCs = 3,
                        NoOfStudents = 100,
                        LogId = 1,
                        MetricFlags = new List<string>
                        {
                            "FlagA",
                            "InvalidFlag",
                        }
                    }
                }
            };

            var metricsFlags = new List<MetricsFlag>
            {
                new MetricsFlag
                {
                    Id = 1,
                    FlagName = "FlagA",
                    FlagCode = "FlagA",
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    MetricsFlagLookups = new List<ApprenticeMetricsFlagData>()
                },
                new MetricsFlag
                {
                    Id = 2,
                    FlagName = "FlagB",
                    FlagCode = "FlagB",
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    MetricsFlagLookups = new List<ApprenticeMetricsFlagData>()
                },
        };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetMetricsFlagQuery>(), new CancellationToken()))
                .ReturnsAsync(metricsFlags);

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLEPSDataByRegionQuery>(), new CancellationToken()))
                .ReturnsAsync(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(expectedResponse.ResultCode.Equals(result.ResultCode));
            Assert.That(result.ValidationErrors.Any(error =>
                ((DetailedValidationError)error).Field.Equals("MetricsFlag", StringComparison.InvariantCultureIgnoreCase) &&
                ((DetailedValidationError)error).Message.Equals("Invalid Metrics Flag in File")));
            _mockMetricsDataRepository.Verify(x => x.AddManyAndDelete(It.IsAny<List<ApprenticeMetricsData>>()), Times.Never);
        }

        [Test]
        public async Task NoRegion_ReturnsNoRegionError()
        {
            var expectedResponse = new CreateMetricsDataResponse
            {
                ResultCode = Responses.ResponseCode.InvalidRequest,
            };

            var command = new CreateMetricsDataCommand
            {
                MetricsData = new List<MetricDto>
                {
                    new MetricDto
                    {
                        Region = "InvalidRegion",
                        IntendedStartYear = 2023,
                        MaxTravelInMiles = 50,
                        WillingnessToRelocate = true,
                        NoOfGCSCs = 3,
                        NoOfStudents = 100,
                        LogId = 1,
                        MetricFlags = new List<string>
                        {
                            "FlagA",
                            "FlagB",
                        }
                    }
                }
            };

            var metricsFlags = new List<MetricsFlag>
            {
                new MetricsFlag
                {
                    Id = 1,
                    FlagName = "FlagA",
                    FlagCode = "FlagA",
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    MetricsFlagLookups = new List<ApprenticeMetricsFlagData>()
                },
                new MetricsFlag
                {
                    Id = 2,
                    FlagName = "FlagB",
                    FlagCode = "FlagB",
                    IsActive = true,
                    DateAdded = DateTime.Now,
                    MetricsFlagLookups = new List<ApprenticeMetricsFlagData>()
                },
        };

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetMetricsFlagQuery>(), new CancellationToken()))
                .ReturnsAsync(metricsFlags);

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetLEPSDataByRegionQuery>(), new CancellationToken()))
                .ReturnsAsync(0);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(expectedResponse.ResultCode.Equals(result.ResultCode));
            Assert.That(result.ValidationErrors.Any(error =>
                ((DetailedValidationError)error).Field.Equals("Region", StringComparison.InvariantCultureIgnoreCase) &&
                ((DetailedValidationError)error).Message.Equals("Invalid Region in File")));
            _mockMetricsDataRepository.Verify(x => x.AddManyAndDelete(It.IsAny<List<ApprenticeMetricsData>>()), Times.Never);
        }
    }
}
