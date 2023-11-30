using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSUserByLepsId
{
    public class GetLEPSUserByLepsIdQuery : IRequest<GetLEPSUsersByLepsIdResult>
    {
        public int LepsId { get; set; }
    }
}
