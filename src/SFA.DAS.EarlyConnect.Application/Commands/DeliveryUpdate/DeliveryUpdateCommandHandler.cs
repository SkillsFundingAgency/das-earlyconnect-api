using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Data.Repository;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.DeliveryUpdate
{
    public class DeliveryUpdateCommandHandler : IRequestHandler<DeliveryUpdateCommand, int>
    {
        private readonly ILogDataRepository _logDataRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IMetricsDataRepository _apprenticeMetricsDataRepository;
        private readonly ISchoolsLeadsDataRepository _schoolsLeadsDataRepository;
        private readonly ISubjectPreferenceDataRepository _subjectPreferenceDataRepository;
        private readonly ILogger<DeliveryUpdateCommandHandler> _logger;
        private readonly IMediator _mediator;

        public DeliveryUpdateCommandHandler(
            ILogDataRepository metricsDataRepository,
            IStudentDataRepository studentDataRepository,
            IMetricsDataRepository apprenticeMetricsDataRepository,
            ISchoolsLeadsDataRepository schoolsLeadsDataRepository,
            ISubjectPreferenceDataRepository subjectPreferenceDataRepository,
            ILogger<DeliveryUpdateCommandHandler> logger,
            IMediator mediator)
        {
            _logDataRepository = metricsDataRepository;
            _studentDataRepository = studentDataRepository;
            _apprenticeMetricsDataRepository = apprenticeMetricsDataRepository;
            _schoolsLeadsDataRepository = schoolsLeadsDataRepository;
            _subjectPreferenceDataRepository = subjectPreferenceDataRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<int> Handle(DeliveryUpdateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating LEPS Date Sent");

            switch (command.Source)
            {
                case "StudentData":
                    await _studentDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
                case "ApprenticeMetricsData":
                    await _apprenticeMetricsDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
                case "SchoolsLeadsData":
                    await _schoolsLeadsDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
                case "SubjectPreferenceData":
                    await _subjectPreferenceDataRepository.UpdateLepsDateSent(command.Ids);
                    break;
            }
            
            return 0;
        }
    }
}
