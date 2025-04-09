using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using SimpleCrypto;
using System.Security.Cryptography;
using System.Text;

namespace IP.Cryptography.PBKDF2Cryptography
{
    public class PBKDF2Cryptography : IPBKDF2Cryptography
    {
        private readonly PBKDF2 cript;
        private readonly ILogsFactory logsFactory;

        public PBKDF2Cryptography(ILogsFactory logsFactory)
        {
            cript = new PBKDF2();
            this.logsFactory = logsFactory;

        }

        public Task<bool> CompareHash(string senha, string hash, string salt)
        {
            try
            {
                var hashResult = CreateHash(senha, salt).Result;
                return Task.FromResult(hashResult.Item1 == hash);
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        //Item 1: hash, Item 2: salt, case is not inserted salt in parameter
        public Task<Tuple<string, string>> CreateHash(string senha, string salt = "")
        {
            try
            {
                byte[] saltBytes = { };
                if (!string.IsNullOrEmpty(salt))
                {
                    var encoding = Encoding.UTF8;
                    saltBytes = encoding.GetBytes(salt);
                }
                else
                    saltBytes = GerarSalt();

                int iteracoes = 10000;
                int tamanhoHashEmBytes = 32;

                byte[] hashBytes = CalcularPBKDF2(senha, saltBytes, iteracoes, tamanhoHashEmBytes);

                if (string.IsNullOrEmpty(salt))
                    salt = Convert.ToBase64String(saltBytes);

                string hashString = Convert.ToBase64String(hashBytes);
                return Task.FromResult(new Tuple<string, string>(hashString, salt));
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        private byte[] GerarSalt()
        {
            // Gera um salt aleatório de 16 bytes
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] CalcularPBKDF2(string senha, byte[] salt, int iteracoes, int tamanhoHashEmBytes)
        {
            // Calcula o hash usando PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, iteracoes))
            {
                return pbkdf2.GetBytes(tamanhoHashEmBytes);
            }
        }
    }
}
