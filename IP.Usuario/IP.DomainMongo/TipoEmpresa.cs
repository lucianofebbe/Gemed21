using IP.BaseDomains;

namespace IP.DomainMongo
{
    public class TipoEmpresa : BaseDomainMongo
    {
        public string Type { get; set; }
        public string ConnectionString { get; set; }
    }
}
