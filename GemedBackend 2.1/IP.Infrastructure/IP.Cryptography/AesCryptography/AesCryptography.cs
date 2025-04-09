using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using System.Security.Cryptography;
using System.Text;

namespace IP.Cryptography.AesCryptography
{
    public class AesCryptography : IAesCryptography
    {
        private readonly ILogsFactory logsFactory;
        private readonly byte[] key = Encoding.UTF8.GetBytes("Gemed2.1.2023");

        public AesCryptography(ILogsFactory logsFactory)
        {
            this.logsFactory = logsFactory;
        }

        public async Task<string> DecryptStringFromBytes_Aes(byte[] cipherText)
        {
            try
            {
                string plaintext = null;
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                return plaintext;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<byte[]> EncryptStringToBytes_Aes(string plainText)
        {
            try
            {
                byte[] encrypted;
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                return encrypted;
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
