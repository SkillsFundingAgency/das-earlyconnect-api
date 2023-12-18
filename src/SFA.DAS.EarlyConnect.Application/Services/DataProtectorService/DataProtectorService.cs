using Microsoft.Extensions.Logging;

namespace SFA.DAS.EarlyConnect.Application.Services.DataProtectorService
{
    public interface IDataProtectorService
    {
        string EncodedData(Guid studentSurveyId, string email);
        Dictionary<Guid, string>? DecodeData(string authCode);
    }

    public class DataProtectorService : IDataProtectorService
    {
        private readonly ILogger<DataProtectorService> _logger;

        public DataProtectorService(ILogger<DataProtectorService> logger)
        {
            _logger = logger;
        }

        public string EncodedData(Guid studentSurveyId, string emai)
        {
            return "authCode";
        }

        public Dictionary<Guid, string>? DecodeData(string authCode)
        {
            // todo

            return new Dictionary<Guid, string>();
        }
    }
}
