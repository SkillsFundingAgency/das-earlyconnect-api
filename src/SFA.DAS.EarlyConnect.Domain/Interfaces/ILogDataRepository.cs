using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ILogDataRepository
    {
        Task<int> CreateAsync(ECAPILog log);
        Task UpdateStatusAndErrorAsync(int logId, string status, string error);
    }
}
