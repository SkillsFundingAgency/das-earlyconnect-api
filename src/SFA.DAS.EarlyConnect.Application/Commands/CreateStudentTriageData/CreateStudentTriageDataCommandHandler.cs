using MediatR;
using Microsoft.Extensions.Logging;
using NServiceBus;
using SFA.DAS.EarlyConnect.Application.Extensions;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;
using SFA.DAS.Notifications.Messages.Commands;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData
{
    public class CreateStudentTriageDataCommandHandler : IRequestHandler<CreateStudentTriageDataCommand, CreateStudentTriageDataResult>
    {
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly IStudentDataRepository _studentDataRepository;
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly ILogger<CreateStudentTriageDataCommandHandler> _logger;
        private readonly IMessageSession _messageSession;
        public const string TemplateId = "EarlyConnectTriageConfirmationEmail";

        public CreateStudentTriageDataCommandHandler(
            IStudentAnswerRepository studentAnswerRepository,
            IStudentDataRepository studentDataRepository,
            IStudentSurveyRepository studentSurveyRepository,
            ILogger<CreateStudentTriageDataCommandHandler> logger,
            IMessageSession messageSession)
        {
            _studentAnswerRepository = studentAnswerRepository;
            _studentDataRepository = studentDataRepository;
            _studentSurveyRepository = studentSurveyRepository;
            _logger = logger;
            _messageSession = messageSession;
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
                    SchoolName = command.StudentData.SchoolName,
                    Industry = command.StudentData.Industry
                });
            }
            catch (ArgumentException ex)
            {
                result.Message = ex.Message;
                return result;
            }

            // update Student Survey
            try
            {
                await _studentSurveyRepository.UpdateStudentSurveyAsync(new StudentSurvey
                {
                    Id = command.StudentSurvey.Id,
                    LastUpdated = command.StudentSurvey.LastUpdated,
                    DateCompleted = command.StudentSurvey.DateCompleted
                });
            }
            catch (ArgumentException ex)
            {
                result.Message = ex.Message;
                return result;
            }

            var answersToCreateAndRemove = command.StudentSurvey.Answers.Where(x => x.QuestionId > 0 && x.AnswerId > 0).ToList().MapFromAnswersDtoToCreate(command.StudentSurveyGuid);

            // 2. AddAndRemove StudentAnswers
            if (answersToCreateAndRemove.Any())
            {
                _logger.LogInformation($"Creating and Removing Student Answers for StudentSurvey {command.StudentSurvey.Id}");

                await _studentAnswerRepository.AddAndRemoveAnswersAsync(command.StudentSurveyGuid, answersToCreateAndRemove);
            }

            if (command.StudentSurvey.DateCompleted != null)
            {
                var tokens = new Dictionary<string, string> { { "Contact", $"{command.StudentData.FirstName} {command.StudentData.LastName}" } };

                _logger.LogInformation($"Sending confirmation email to student");

                await _messageSession.Send(new SendEmailCommand(TemplateId, command.StudentData.Email, tokens));
            }

            result.Message = "Success";

            return result;
        }
    }
}
