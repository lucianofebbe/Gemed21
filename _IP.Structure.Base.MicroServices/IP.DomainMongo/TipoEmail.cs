using IP.BaseDomains;

namespace IP.DomainMongo
{
    public class TipoEmail : BaseDomainMongo
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }
}
