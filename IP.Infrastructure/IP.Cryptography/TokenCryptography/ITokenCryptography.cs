namespace IP.Cryptography.TokenCryptography
{
    public interface ITokenCryptography
    {
        Task<string> GenerateToken(string value);
        Task<string> GenerateToken(int value);
    }
}
