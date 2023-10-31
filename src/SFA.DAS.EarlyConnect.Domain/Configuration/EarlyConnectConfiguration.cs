namespace SFA.DAS.EarlyConnect.Domain.Configuration
{
    public class EarlyConnectConfiguration
    {
        public EarlyConnectApi EarlyConnectApi { get; set; }
        public AzureAd AzureAd { get; set; }
    }

    public class EarlyConnectApi
    {
        public string? DatabaseConnectionString { get; set; }
    }

    public class AzureAd
    {
        public string Tenant { get; set; }
        public string Identifier { get; set; }
    }
}
