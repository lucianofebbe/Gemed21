namespace IP.Logs.LogsSettings
{

    public class LogsSettings
    {
        public bool SendMail { get; private set; }
        public TypeLogs TypeLog { get; private set; }

        public LogsSettings(bool SendMail, TypeLogs TypeLog)
        {
            this.SendMail = SendMail;
            this.TypeLog = TypeLog;
        }

    }

    public enum TypeLogs
    {
        System,
        Authentication,
        Permissions,
        General
    }
}
