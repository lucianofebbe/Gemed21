using IP.BaseDomains;

namespace IP.MongoDb.Domains
{
    public class TipoRequisicao : BaseDomainMongo
    {
        public string Type { get; set; }
        public string Servico { get; set; }
        public string HostName { get; set; }
        public string HostNameDesenv { get; set; }
    }
}
