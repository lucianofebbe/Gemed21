using IP.MongoDb.MongoDbConfig;

namespace IP.Mails.Mails
{
    public interface IMails
    {
        Task SendMail();
    }
}
