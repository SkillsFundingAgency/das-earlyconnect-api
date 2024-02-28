using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetStudentIdBySurveyId
{
    public class GetStudentIdBySurveyIdQuery : IRequest<int>
    {
        public Guid StudentSurveyId { get; set; }
    }
}
