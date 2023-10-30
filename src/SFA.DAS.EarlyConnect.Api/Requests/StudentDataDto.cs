namespace SFA.DAS.EarlyConnect.Api.Requests
{
    public class StudentDataDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Postcode { get; set; }
        public string Industry { get; set; }
        public DateTime? DateOfInterest { get; set; }
    }
}