namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class LEPSUser
    {
        public int Id { get; set; }
        public int LepsId { get; set; }// FK to LEPSData
        public virtual LEPSData LepsData { get; set; } // Navigation Property to LEPSData
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public string GDPRCompliance { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
