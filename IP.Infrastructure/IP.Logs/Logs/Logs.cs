using IP.Mails.MailsFactory;
using IP.MongoDb.MongoDbFactory;
using System.Diagnostics;
using System.Text;

namespace IP.Logs.Logs
{
    public class Logs : ILogs
    {
        private readonly IMongoDbFactory<LogsConfig.LogsConfig> mongoDb;
        private readonly IMailsFactory mails;
        private readonly LogsSettings.LogsSettings settings;
        public Logs(
            LogsSettings.LogsSettings settings,
            IMongoDbFactory<LogsConfig.LogsConfig> mongoDb,
            IMailsFactory mails)    
        {
            this.mongoDb = mongoDb;
            this.mails = mails;
            this.settings = settings;
        }

        public async Task GenerateLog(Exception ex, string message = "")
        {
            try
            {
                var stkFrame = new StackFrame(1).GetMethod();
                var logConfig = new LogsConfig.LogsConfig()
                {
                    Data = DateTime.Now.ToString(),
                    Mensage = message,
                    Metodo = stkFrame != null ? stkFrame.Name : "",
                    Erro = ex.ToString()
                };
                var logs = mongoDb.Create("Logs");
                await logs.Insert(logConfig, new CancellationToken());
                SendingMail(logConfig);
            }
            catch(Exception exc)
            {
                throw new Exception("", exc);
            }
        }
        private async void SendingMail(LogsConfig.LogsConfig logConfig)
        {
            if (settings.SendMail)
            {
                var email = mails.Create();
                await email.SendMail(new Mails.MailsSettings.MailsSettings()
                {
                    Provider = "Gmail",
                    Body = GenerateLogToMail(logConfig),
                    BodyHtml = true,
                    Subject = "GeneratorLog - Log ErroAplication",
                    ToEmail = "lucianofebbedossantos@hotmail.com"
                });
            }
        }
        private string GenerateLogToMail(LogsConfig.LogsConfig config)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("Data: " + config.Data + "." + Environment.NewLine);
            strBuilder.Append("Mensage: " + config.Mensage + "." + Environment.NewLine);
            strBuilder.Append("Método: " + config.Metodo + "." + Environment.NewLine);
            strBuilder.Append("Erro: " + config.Erro + "." + Environment.NewLine);
            strBuilder.Append("----------------------------------------------------//");
            strBuilder.Append("----------------------------------------------------" + Environment.NewLine);
            return strBuilder.ToString();
        }
    }
}
