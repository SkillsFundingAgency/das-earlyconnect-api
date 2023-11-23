using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsCode
{
    public class GetLEPSDataByLepsCodeQuery : IRequest<LEPSDataDto>
    {
        public string LEPSCode { get; set; }
    }
}
