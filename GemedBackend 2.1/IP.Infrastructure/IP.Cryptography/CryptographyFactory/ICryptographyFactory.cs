using IP.Cryptography.TokenCryptography.TokenCryptographySettings;

namespace IP.Cryptography.CryptographyFactory
{
    public interface ICryptographyFactory
    {
        AesCryptography.AesCryptography CreateAesCryptography();
        PBKDF2Cryptography.PBKDF2Cryptography CreatePBKDF2Cryptography();
        TokenCryptography.TokenCryptography CreateTokenCryptography();

    }
}
