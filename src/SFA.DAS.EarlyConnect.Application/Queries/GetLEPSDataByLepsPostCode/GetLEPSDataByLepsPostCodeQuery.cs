using MediatR;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsPostCode
{
    public class GetLEPSDataByLepsPostCodeQuery : IRequest<int>
    {
        public string PostCode { get; set; }
    }
}
