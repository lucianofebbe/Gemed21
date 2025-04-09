using IP.BaseDomains;

namespace IP.MongoDb.Domains
{
    public class TipoEmpresa : BaseDomainMongo
    {
        public string Type { get; set; }
        public string Company { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }
}
