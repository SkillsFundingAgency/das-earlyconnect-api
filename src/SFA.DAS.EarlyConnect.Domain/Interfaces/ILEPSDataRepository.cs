namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface ILEPSDataRepository
    {
        Task<int> GetLepsIdByRegionAsync(string region);
    }
}
