using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ISchoolsLeadsDataRepository
    {
        Task UpdateLepsDateSent(IList<int> ids);
    }
}