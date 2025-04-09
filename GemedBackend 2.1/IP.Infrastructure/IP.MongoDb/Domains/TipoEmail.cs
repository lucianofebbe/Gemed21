using IP.BaseDomains;

namespace IP.MongoDb.Domains
{
    public class TipoEmail : BaseDomainMongo
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }
}
