using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.EarlyConnect.Application.Commands.CreateLog;
using SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Commands.UpdateLog;
using SFA.DAS.EarlyConnect.Application.Services.AuthCodeService;
using SFA.DAS.EarlyConnect.Application.Services.DataProtectorService;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData
{
    public class CreateStudentTriageDataCommandHandler : IRequestHandler<CreateStudentTriageDataCommand, Unit>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly ILogger<CreateStudentTriageDataCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateStudentTriageDataCommandHandler(
            ISurveyRepository surveyRepository,
            IStudentDataRepository studentDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            ILogger<CreateStudentTriageDataCommandHandler> logger,
            IMediator mediator)
        {
            _surveyRepository = surveyRepository;
            _studentDataRepository = studentDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateStudentTriageDataCommand command, CancellationToken cancellationToken)
        {
            // 1. Update Student Data
            await _studentDataRepository.UpdateAsync(new StudentData
            {
                FirstName = command.StudentData.FirstName,
                LastName = command.StudentData.LastName,
                LogId = command.StudentData.LogId,
                LepsId = command.StudentData.LepsId,
                DateOfBirth = command.StudentData.DateOfBirth,
                Email = command.StudentData.Email,
                Postcode = command.StudentData.Postcode,
                DataSource = command.StudentData.DataSource,
                Industry = command.StudentData.Industry,
                DateInterestShown = command.StudentData.DateOfInterest
            });

            // 2. Update Student Survey

            // 3. Add StudentAnswers

            return Unit.Value;
        }
    }
}
