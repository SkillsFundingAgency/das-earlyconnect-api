namespace SFA.DAS.EarlyConnect.Application.Services.AuthCodeService
{
    public interface IAuthCodeService
    {
        string Generate6DigitCode();
    }

    internal class AuthCodeService : IAuthCodeService
    {
        private readonly Random _rand;

        public AuthCodeService()
        {
            _rand = new Random(Guid.NewGuid().GetHashCode());
        }

        public string Generate6DigitCode()
        {
            var authCode = _rand.Next(100000, 999999);
            return authCode.ToString();
        }
    }
}
