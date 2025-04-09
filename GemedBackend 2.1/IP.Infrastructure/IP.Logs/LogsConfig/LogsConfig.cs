using IP.BaseDomains;
using IP.Logs.LogsSettings;

namespace IP.Logs.LogsConfig
{
    public class LogsConfig : BaseDomainMongo
    {
        public string Data { get; set; }
        public string Mensage { get; set; }
        public string Metodo { get; set; }
        public string Erro { get; set; }
        public string TypeLog { get; set; }
    }
}
