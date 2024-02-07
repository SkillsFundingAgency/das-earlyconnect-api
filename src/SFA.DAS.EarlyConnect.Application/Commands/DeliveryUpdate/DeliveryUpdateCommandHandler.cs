using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Application.Responses;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.DeliveryUpdate
{
    public class DeliveryUpdateCommandHandler : IRequestHandler<DeliveryUpdateCommand, DeliveryUpdateResult>
    {
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IMetricsDataRepository _apprenticeMetricsDataRepository;
        private readonly ISchoolsLeadsDataRepository _schoolsLeadsDataRepository;
        private readonly ISubjectPreferenceDataRepository _subjectPreferenceDataRepository;
        private readonly ILogger<DeliveryUpdateCommandHandler> _logger;
        private readonly IMediator _mediator;

        public DeliveryUpdateCommandHandler(
            IStudentDataRepository studentDataRepository,
            IMetricsDataRepository apprenticeMetricsDataRepository,
            ISchoolsLeadsDataRepository schoolsLeadsDataRepository,
            ISubjectPreferenceDataRepository subjectPreferenceDataRepository,
            ILogger<DeliveryUpdateCommandHandler> logger,
            IMediator mediator)
        {
            _studentDataRepository = studentDataRepository;
            _apprenticeMetricsDataRepository = apprenticeMetricsDataRepository;
            _schoolsLeadsDataRepository = schoolsLeadsDataRepository;
            _subjectPreferenceDataRepository = subjectPreferenceDataRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<DeliveryUpdateResult> Handle(DeliveryUpdateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating LEPS Date Sent");

            var invalidIds = new List<int>();

            switch (command.Source)
            {
                case "StudentData":
                    invalidIds = await _studentDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
                case "ApprenticeMetricsData":
                    invalidIds = await _apprenticeMetricsDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
                case "SchoolsLeadsData":
                    invalidIds = await _schoolsLeadsDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
                case "SubjectPreferenceData":
                    invalidIds = await _subjectPreferenceDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
            }

            var logStatus = invalidIds.Any() ? "Error" : "Completed";
            var errorMessage = invalidIds.Any() ? "" : string.Empty;

            await _mediator.Send(new UpdateLogCommand
            {
                LogId = command.LogId,
                Status = logStatus,
                Error = errorMessage
            });

            DeliveryUpdateResult commandResult = new DeliveryUpdateResult();

            if(invalidIds.Any())
            {
                commandResult.ResultCode = ResponseCode.InvalidRequest;
                commandResult.ValidationErrors = new List<DetailedValidationError>
                { 
                    new DetailedValidationError
                    {
                        Field = "Ids", Message = "Invalid Ids in File " + string.Join(",", invalidIds)
                    }
                }.Cast<object>().ToList();
            }
            else
            {
                commandResult.ResultCode = ResponseCode.Success;
            }

            return commandResult;

        }
    }
}
