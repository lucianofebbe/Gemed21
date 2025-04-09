using IP.Cryptography.TokenCryptography.TokenCryptographySettings;

namespace IP.Cryptography.TokenCryptography
{
    public interface ITokenCryptography
    {
        Task<string> GenerateToken(JwtConfigurationSettings settings);
    }
}
