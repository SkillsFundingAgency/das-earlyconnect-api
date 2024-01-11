using MediatR;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsPostCode
{
    public class GetLEPSDataByLepsPostCodeQueryHandler : IRequestHandler<GetLEPSDataByLepsPostCodeQuery, int>
    {
        private readonly ILEPSDataRepository _lepsDataRepository;

        public GetLEPSDataByLepsPostCodeQueryHandler(ILEPSDataRepository lepsDataRepository)
        {
            _lepsDataRepository = lepsDataRepository;
        }

        public async Task<int> Handle(GetLEPSDataByLepsPostCodeQuery request, CancellationToken cancellationToken)
        {
            var lepsId = await _lepsDataRepository.GetLepsIdByPostCodeAsync(request.PostCode);

            return lepsId;
        }
    }
}
