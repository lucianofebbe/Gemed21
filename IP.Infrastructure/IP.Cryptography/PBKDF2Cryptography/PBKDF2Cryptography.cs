using SimpleCrypto;

namespace IP.Cryptography.PBKDF2Cryptography
{
    public class PBKDF2Cryptography : IPBKDF2Cryptography
    {
        private readonly PBKDF2 cript;

        public PBKDF2Cryptography() {
            cript = new PBKDF2();
        }
        public Task<bool> CompareHash(string Hash1, string Hash2)
        {
            var result = cript.Compute(Hash1, Hash2);
            return Task.FromResult(!cript.Compare(Hash1, result));
        }
    }
}
