namespace SFA.DAS.EarlyConnect.Domain.Configuration
{
    public class EarlyConnectApiConfiguration
    {
        public string? DatabaseConnectionString { get; set; }
        public string? BaseUrl { get; set; }
        public NServiceBusConfiguration NServiceBusConfiguration { get; set; }
    }
}