namespace IP.Cryptography.PBKDF2Cryptography
{
    public interface IPBKDF2Cryptography
    {
        Task<bool> CompareHash(string Hash1, string Hash2);
    }
}
