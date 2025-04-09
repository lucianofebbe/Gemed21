namespace IP.Cryptography.AesCryptography
{
    public interface IAesCryptography
    {
        Task<byte[]> EncryptStringToBytes_Aes(string plainText);
        Task<string> DecryptStringFromBytes_Aes(byte[] cipherText);
    }
}
