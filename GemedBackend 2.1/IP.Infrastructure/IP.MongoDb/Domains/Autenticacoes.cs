using IP.BaseDomains;

namespace IP.MongoDb.Domains
{
    public class Autenticacoes : BaseDomainMongo
    {
        public int IdUsuario { get; set; }
        public DateTime UltimoLogin { get; set; }
        public bool Logado { get; set; }
        public string Dispositivo { get; set; }
        public string RefreshToken { get; set; }
    }
}
