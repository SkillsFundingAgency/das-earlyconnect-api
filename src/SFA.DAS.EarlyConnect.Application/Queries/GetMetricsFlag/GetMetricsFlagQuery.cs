using MediatR;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetMetricsFlag
{
    public class GetMetricsFlagQuery : IRequest<IEnumerable<MetricsFlag>>
    {
    }
}
