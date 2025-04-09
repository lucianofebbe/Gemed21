using IP.MongoDb.MongoDbConfig;

namespace IP.Logs.LogsFactory
{
    public interface ILogsFactory
    {
        Logs.Logs Create(LogsSettings.LogsSettings settings, Exception ex, MongoConfig config, string message);
    }
}
