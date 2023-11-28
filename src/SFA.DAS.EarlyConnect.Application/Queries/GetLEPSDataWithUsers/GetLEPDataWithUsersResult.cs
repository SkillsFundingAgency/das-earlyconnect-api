using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSDataWithUsers
{
    public class GetLEPDataWithUsersResult
    {
        public ICollection<LEPSDataDto> LEPSData { get; set; }
    }
}
