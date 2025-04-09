using IP.Logs.LogsFactory;

namespace IP.Cryptography.CryptographyFactory
{
    public class CryptographyFactory : ICryptographyFactory
    {
        public AesCryptography.AesCryptography CreateAesCryptography()
        {
            return new AesCryptography.AesCryptography(new LogsFactory());
        }

        public PBKDF2Cryptography.PBKDF2Cryptography CreatePBKDF2Cryptography()
        {
            return new PBKDF2Cryptography.PBKDF2Cryptography(new LogsFactory());
        }

        public TokenCryptography.TokenCryptography CreateTokenCryptography()
        {
            return new TokenCryptography.TokenCryptography(new LogsFactory());
        }
    }
}
