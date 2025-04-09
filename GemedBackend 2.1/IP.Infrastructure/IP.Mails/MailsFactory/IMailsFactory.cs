using IP.Mails.Mails;
using IP.MongoDb.MongoDbConfig;

namespace IP.Mails.MailsFactory
{
    public interface IMailsFactory
    {
        Mails.Mails Create(MailsSettings.MailsSettings settings, MongoConfig config);
    }
}
