using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Extensions;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData
{
    public class CreateStudentTriageDataCommandHandler : IRequestHandler<CreateStudentTriageDataCommand, Unit>
    {
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly ILogger<CreateStudentTriageDataCommandHandler> _logger;

        public CreateStudentTriageDataCommandHandler(
            IStudentAnswerRepository studentAnswerRepository,
            IStudentDataRepository studentDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            ILogger<CreateStudentTriageDataCommandHandler> logger)
        {
            _studentAnswerRepository = studentAnswerRepository;
            _studentDataRepository = studentDataRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateStudentTriageDataCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Student Data during the Student Triage Data journey");

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
                Telephone = command.StudentData.Telephone,
                DataSource = command.StudentData.DataSource,
                SchoolName = command.StudentData.SchoolName,
                Industry = command.StudentData.Industry
            });

            var answersToCreate = command.StudentSurvey.Answers.Where(x => x.Id == null).ToList().MapFromAnswersDtoToCreate();
            var answersToUpdate = command.StudentSurvey.Answers.Where(x => x.Id != null).ToList().MapFromAnswersDtoToUpdate();

            // 2. Add StudentAnswers
            if (answersToCreate.Any()) 
            {
                _logger.LogInformation($"Creating Student Answers for StudentSurvey {command.StudentSurvey.Id}");

                await _studentAnswerRepository.AddManyAsync(answersToCreate);
            }

            // 3. Update StudentAnswers
            if (answersToUpdate.Any())
            {
                _logger.LogInformation($"Updating Student Answers for StudentSurvey {command.StudentSurvey.Id}");

                foreach (var answer in answersToUpdate) 
                {
                    await _studentAnswerRepository.UpdateAsync(answer);
                }
            }

            return Unit.Value;
        }

  
    }
}
