using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ISchoolsLeadsDataRepository
    {
        Task<List<int>> UpdateLepsDateSent(IList<int> ids);
    }
}