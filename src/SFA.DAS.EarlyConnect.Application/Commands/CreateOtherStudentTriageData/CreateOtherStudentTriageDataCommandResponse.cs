using SFA.DAS.EarlyConnect.Application.Responses;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateOtherStudentTriageData
{
    public class CreateOtherStudentTriageDataCommandResponse : BaseResponse
    {
        public string StudentSurveyId { get; set; }
        public string AuthCode { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
