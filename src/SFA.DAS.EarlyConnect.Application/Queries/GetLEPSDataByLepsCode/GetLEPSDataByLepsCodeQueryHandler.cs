using MediatR;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataByLepsCode
{
    public class GetLEPSDataByLepsCodeQueryHandler : IRequestHandler<GetLEPSDataByLepsCodeQuery, LEPSDataDto>
    {
        private readonly ILEPSDataRepository _lepsDataRepository;

        public GetLEPSDataByLepsCodeQueryHandler(ILEPSDataRepository lepsDataRepository)
        {
            _lepsDataRepository = lepsDataRepository;
        }

        public async Task<LEPSDataDto> Handle(GetLEPSDataByLepsCodeQuery request, CancellationToken cancellationToken)
        {
            var lepsId = await _lepsDataRepository.GetLepsIdByLepsCodeAsync(request.LEPSCode);
            var region = await _lepsDataRepository.GetLepsRegionByLepsCodeAsync(request.LEPSCode);

            return new LEPSDataDto
            {
                LepId = lepsId,
                Region = region
            };
        }
    }
}
