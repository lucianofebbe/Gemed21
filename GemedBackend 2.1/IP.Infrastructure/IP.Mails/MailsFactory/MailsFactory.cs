using IP.Mails.Mails;
using IP.MongoDb.MongoDbConfig;

namespace IP.Mails.MailsFactory
{
    public class MailsFactory : IMailsFactory
    {
        public Mails.Mails Create(MailsSettings.MailsSettings settings, MongoConfig config)
        {
            var factory = new IP.MongoDb.MongoDbFactory.MongoDbFactory<MailsConfig.MailsConfig>();
            return new Mails.Mails(factory, settings, config);
        }
    }
}
