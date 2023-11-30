using SFA.DAS.EarlyConnect.Application.Models;

namespace SFA.DAS.EarlyConnect.Application.Queries.GetLEPSUserByLepsId
{
    public class GetLEPSUsersByLepsIdResult
    {
        public ICollection<LEPSUserDto> LEPSUsers { get; set; }
    }
}
