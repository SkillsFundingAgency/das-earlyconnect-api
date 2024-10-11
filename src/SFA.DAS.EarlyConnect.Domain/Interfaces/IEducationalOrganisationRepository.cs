using SFA.DAS.EarlyConnect.Domain.Dtos;

namespace SFA.DAS.EarlyConnect.Domain.Interfaces
{
    public interface IEducationalOrganisationRepository
    {
        Task<List<EducationalOrganisation>> GetNameByLepCodeAsync(string lepCode, string name,int page , int pageSize);
    }
}