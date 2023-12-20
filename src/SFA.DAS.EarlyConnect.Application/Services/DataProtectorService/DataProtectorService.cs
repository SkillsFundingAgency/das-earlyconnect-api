using Microsoft.Extensions.Logging;

namespace SFA.DAS.EarlyConnect.Application.Services.DataProtectorService
{
    public interface IDataProtectorService
    {
        string EncodedData(string authCode);
        string DecodeData(string encryptedAuthCode);
    }

    public class DataProtectorService : IDataProtectorService
    {
        private readonly ILogger<DataProtectorService> _logger;

        public DataProtectorService(ILogger<DataProtectorService> logger)
        {
            _logger = logger;
        }

        public string EncodedData(string authCode)
        {
            return "authCode";
        }

        public string DecodeData(string encryptedAuthCode)
        {
            // todo

            return "decrypted";
        }
    }
}
