namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class LEPSCoverage
    {
        public int Id { get; set; }
        public int LEPSId { get; set; }
        public string Postcode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual LEPSData LepsData { get; set; } // Navigation Property to LEPSData
    }
}