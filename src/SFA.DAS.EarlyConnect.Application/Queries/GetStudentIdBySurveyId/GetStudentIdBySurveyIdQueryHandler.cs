using MediatR;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetStudentIdBySurveyId
{
    public class GetStudentIdBySurveyIdQueryHandler : IRequestHandler<GetStudentIdBySurveyIdQuery, int>
    {
        private readonly IStudentSurveyRepository _studentSurveyRepository;

        public GetStudentIdBySurveyIdQueryHandler(IStudentSurveyRepository studentSurveyRepository)
        {
            _studentSurveyRepository = studentSurveyRepository;
        }

        public async Task<int> Handle(GetStudentIdBySurveyIdQuery request, CancellationToken cancellationToken)
        {
            var studentSurvey = await _studentSurveyRepository.GetStudentSurveyBySurveyIdAsync(request.StudentSurveyId);

            return studentSurvey?.StudentId ?? 0;
        }
    }
}
