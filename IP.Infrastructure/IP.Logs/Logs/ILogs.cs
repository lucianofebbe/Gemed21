namespace IP.Logs.Logs
{
    public interface ILogs
    {
        public Task GenerateLog(Exception ex, string message = "");
    }
}
