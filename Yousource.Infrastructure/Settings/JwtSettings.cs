namespace Yousource.Infrastructure.Settings
{
    public class JwtSettings
    {
        public JwtSettings(string secret, int expiresInMinutes = 1440)
        {
            this.Secret = secret;
            this.ExpiresInMinutes = expiresInMinutes;
        }

        public string Secret { get; private set; }

        public int ExpiresInMinutes { get; private set; }
    }
}
