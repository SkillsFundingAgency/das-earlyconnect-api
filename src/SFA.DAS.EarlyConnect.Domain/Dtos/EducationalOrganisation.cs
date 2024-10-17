namespace SFA.DAS.EarlyConnect.Domain.Dtos
{
    public class EducationalOrganisationsData
    {
        public int TotalCount { get; set; }
        public ICollection<EducationalOrganisation> EducationalOrganisations { get; set; }
    }
    public class EducationalOrganisation
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string URN { get; set; }
    }
}
