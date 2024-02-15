using IP.Mails.MailsFactory;
using IP.MongoDb.MongoDbFactory;

namespace IP.Logs.LogsFactory
{
    public class LogsFactory : ILogsFactory
    {
        public Logs.Logs Create(LogsSettings.LogsSettings settings)
        {
            return new Logs.Logs(settings,
                new MongoDbFactory<LogsConfig.LogsConfig>(),
                new MailsFactory());
        }
    }
}
