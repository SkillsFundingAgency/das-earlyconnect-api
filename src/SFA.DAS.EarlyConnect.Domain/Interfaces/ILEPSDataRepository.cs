using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ILEPSDataRepository
    {
        Task<int> GetLepsIdByRegionAsync(string region);
        Task<int> GetLepsIdByPostCodeAsync(string postCode);
        Task<int> GetLepsIdByLepsCodeAsync(string lepsCode);
        Task<string> GetLepsRegionByLepsCodeAsync(string lepsCode);
        Task<ICollection<LEPSData>> GetAllLepsDataAsync();
    }
}
