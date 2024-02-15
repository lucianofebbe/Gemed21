namespace IP.Logs.LogsFactory
{
    public interface ILogsFactory
    {
        Logs.Logs Create(LogsSettings.LogsSettings settings);
    }
}
