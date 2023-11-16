using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSData
{
    public class GetLEPSDataByRegionQueryHandler : IRequestHandler<GetLEPSDataByRegionQuery, int>
    {
        private readonly ILEPSDataRepository _lepsDataRepository;

        public GetLEPSDataByRegionQueryHandler(ILEPSDataRepository lepsDataRepository)
        {
            _lepsDataRepository = lepsDataRepository;
        }

        public async Task<int> Handle(GetLEPSDataByRegionQuery request, CancellationToken cancellationToken)
        {
            var lepsId = await _lepsDataRepository.GetLepsIdByRegionAsync(request.Region);

            return lepsId;
        }
    }
}
