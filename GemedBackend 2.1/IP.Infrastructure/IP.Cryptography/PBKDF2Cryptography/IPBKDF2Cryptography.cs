namespace IP.Cryptography.PBKDF2Cryptography
{
    public interface IPBKDF2Cryptography
    {
        Task<bool> CompareHash(string senha, string hash, string salt);
        Task<Tuple<string, string>> CreateHash(string senha, string salt);
    }
}
