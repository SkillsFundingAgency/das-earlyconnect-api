using MediatR;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentTriageData
{
    public class CreateStudentTriageDataCommand : IRequest<CreateStudentTriageDataResult>
    {
        public Guid StudentSurveyGuid { get; set; }
        public StudentDataDto StudentData { get; set; }
        public StudentSurveyDto StudentSurvey { get; set; }
    }
}
