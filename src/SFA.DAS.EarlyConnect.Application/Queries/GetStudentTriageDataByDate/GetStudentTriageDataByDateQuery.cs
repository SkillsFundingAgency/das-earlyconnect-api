using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetStudentDataTriageByDate
{
    public class GetStudentDataTriageByDateQuery : IRequest<GetStudentDataTriageByDateResult>
    {
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }        
    }
}