using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ILEPSUserRepository
    {
        Task<ICollection<LEPSUser>> GetLepsUsersByLepsIdAsync(int lepsId);
    }
}
