namespace IP.Cryptography.TokenCryptography
{
    public interface IJwtConfiguration
    {
        string AuthIssuer { get; }
        string AuthAudience { get; }
        string AuthSecret { get; }
    }

    public class JwtConfiguration : IJwtConfiguration
    {
        string AuthIssuer { get; }

        string IJwtConfiguration.AuthIssuer => throw new NotImplementedException();

        string AuthAudience { get; }

        string IJwtConfiguration.AuthAudience => throw new NotImplementedException();

        string AuthSecret { get; }

        string IJwtConfiguration.AuthSecret => throw new NotImplementedException();
    }
}
