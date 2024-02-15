using IP.BaseDomains;

namespace IP.Mails.MailsConfig
{
    public class MailsConfig : BaseDomainMongo
    {
        public string Provider { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
    }
}
