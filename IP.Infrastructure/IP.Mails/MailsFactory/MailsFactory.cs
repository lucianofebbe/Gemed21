using IP.Mails.Mails;

namespace IP.Mails.MailsFactory
{
    public class MailsFactory : IMailsFactory
    {
        public Mails.Mails Create()
        {
            var factory = new IP.MongoDb.MongoDbFactory.MongoDbFactory<MailsConfig.MailsConfig>();
            return new Mails.Mails(factory);
        }
    }
}
