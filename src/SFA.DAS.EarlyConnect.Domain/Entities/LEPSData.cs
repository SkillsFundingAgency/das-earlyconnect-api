namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class LEPSData
    {
        public int Id { get; set; }
        public string LepCode { get; set; }
        public string LepName { get; set; }
        public string EntityEmail { get; set; }
        public string Region { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public string TechnicalContant { get; set; }
        public string PostAPIUrl { get; set; }
        public string APIKeyName { get; set; }
        public string APIKeyValue { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
