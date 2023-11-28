namespace SFA.DAS.EarlyConnect.Application.Models
{
    public class LEPSDataDto
    {
        public int Id { get; set; }
        public string LepCode { get; set; }
        public string Region { get; set; }
        public string LepName { get; set; }
        public string EntityEmail { get; set; }
        public string Postcode { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<LEPSUserDto>? LEPSUsers { get; set; }
    }
}
