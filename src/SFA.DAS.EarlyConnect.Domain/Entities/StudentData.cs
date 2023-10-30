namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class StudentData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Postcode { get; set; }
        public string Industry { get; set; }
        public DateTime? DateInterestShown { get; set; }
        public int? LepId { get; set; }
        public DateTime? LepDateSent { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
