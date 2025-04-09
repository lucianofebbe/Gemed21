using IP.Mails.MailsFactory;
using IP.MongoDb.MongoDbConfig;
using IP.MongoDb.MongoDbFactory;

namespace IP.Logs.LogsFactory
{
    public class LogsFactory : ILogsFactory
    {
        public Logs.Logs Create(LogsSettings.LogsSettings settings, Exception ex, MongoConfig config, string message)
        {
            return new Logs.Logs(
                new MongoDbFactory<LogsConfig.LogsConfig>(),
                new MailsFactory(),
                settings,
                ex,
                config,
                message);
        }
    }
}
