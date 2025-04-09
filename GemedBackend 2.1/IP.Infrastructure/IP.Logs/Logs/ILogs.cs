using IP.MongoDb.MongoDbConfig;

namespace IP.Logs.Logs
{
    public interface ILogs
    {
        public Task GenerateLog();
    }
}
