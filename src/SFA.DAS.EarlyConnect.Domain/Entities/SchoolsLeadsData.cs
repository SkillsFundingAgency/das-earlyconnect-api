namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class SchoolsLeadsData
    {
        public int Id { get; set; }
        public int LogId { get; set; }//FK EcapLog
        public int LEPSId { get; set; }//FK LEPS
        public string OrganisationName { get; set; }
        public int NoOfYear13Students { get; set; }
        public DateTime? ConvertedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool HasUCASPortalAccess { get; set; }
        public string CurrentServices { get; set; }
        public string OtherServices { get; set; }
        public string LeadStatus { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LepDateSent { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
