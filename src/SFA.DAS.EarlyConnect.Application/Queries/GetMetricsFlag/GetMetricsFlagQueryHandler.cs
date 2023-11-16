using MediatR;
using SFA.DAS.EarlyConnect.Domain.Entities;
using SFA.DAS.EarlyConnect.Domain.Interfaces;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetMetricsFlag
{
    public class GetMetricsFlagQueryHandler : IRequestHandler<GetMetricsFlagQuery, IEnumerable<MetricsFlag>>
    {
        private readonly IMetricsFlagRepository _metricsFlagRepository;

        public GetMetricsFlagQueryHandler(IMetricsFlagRepository metricsFlagRepository)
        {
            _metricsFlagRepository = metricsFlagRepository;
        }

        public async Task<IEnumerable<MetricsFlag>> Handle(GetMetricsFlagQuery request, CancellationToken cancellationToken)
        {
            var response = await _metricsFlagRepository.GetMetricsFlagAsync();

            return response;
        }
    }
}
