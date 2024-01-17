using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Extensions;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData
{
    public class CreateStudentTriageDataCommandHandler : IRequestHandler<CreateStudentTriageDataCommand, CreateStudentTriageDataResult>
    {
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly ILogger<CreateStudentTriageDataCommandHandler> _logger;

        public CreateStudentTriageDataCommandHandler(
            IStudentAnswerRepository studentAnswerRepository,
            IStudentDataRepository studentDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            ILogger<CreateStudentTriageDataCommandHandler> logger)
        {
            _studentAnswerRepository = studentAnswerRepository;
            _studentDataRepository = studentDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _logger = logger;
        }

        public async Task<CreateStudentTriageDataResult> Handle(CreateStudentTriageDataCommand command, CancellationToken cancellationToken)
        {
            CreateStudentTriageDataResult result = new CreateStudentTriageDataResult();

            _logger.LogInformation("Updating Student Data during the Student Triage Data journey");

            // 1. Update Student Data
            try
            {
                await _studentDataRepository.UpdateAsync(new StudentData
                {
                    Id = command.StudentData.Id,
                    FirstName = command.StudentData.FirstName,
                    LastName = command.StudentData.LastName,
                    DateOfBirth = command.StudentData.DateOfBirth,
                    Email = command.StudentData.Email,
                    Postcode = command.StudentData.Postcode,
                    Telephone = command.StudentData.Telephone,
                    DataSource = command.StudentData.DataSource,
                    SchoolName = command.StudentData.SchoolName,
                    Industry = command.StudentData.Industry
                });
            } 
            catch (ArgumentException ex) 
            {
                result.Message = ex.Message;
                return result;
            }

            // checkif studentSurvey exists
            try
            {
                var studentSurvey = await _studentSurveyRepository.GetByIdAsync(command.StudentSurveyGuid);
            }
            catch (ArgumentException ex)
            {
                result.Message = ex.Message;
                return result;
            }

            var answersToCreate = command.StudentSurvey.Answers.Where(x => x.Id == null).ToList().MapFromAnswersDtoToCreate(command.StudentSurveyGuid);
            var answersToUpdate = command.StudentSurvey.Answers.Where(x => x.Id != null).ToList().MapFromAnswersDtoToUpdate(command.StudentSurveyGuid);

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
            result.Message = "Success";

            return result;
        }

  
    }
}
