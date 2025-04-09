using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Cryptography.CryptographyFactory;
using IP.Cryptography.TokenCryptography.TokenCryptographySettings;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mails.MailsFactory;
using IP.MongoDb.Domains;
using IP.MongoDb.MongoDbFactory;
using MongoDB.Driver;

namespace IP.Services.Usuario.SendInvite
{
    public class SendInviteService : ISendInviteService
    {
        private readonly UsuarioSendInviteRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly ICryptographyFactory iCryptographyFactory;
        private readonly IMongoDbFactory<TipoEmail> iMongoDbFactory;
        private readonly IMailsFactory iMailsFactory;
        private readonly ILogsFactory iLogsFactory;

        public SendInviteService(
            UsuarioSendInviteRequest request,
            CancellationToken cancellationToken,
            ICryptographyFactory iCryptographyFactory,
            IMongoDbFactory<TipoEmail> iMongoDbFactory,
            IMailsFactory iMailsFactory,
            ILogsFactory iLogsFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iMongoDbFactory = iMongoDbFactory;
            this.iCryptographyFactory = iCryptographyFactory;
            this.iMailsFactory = iMailsFactory;
            this.iLogsFactory = iLogsFactory;
        }

        public async Task<UsuarioSendInviteResponse> SendInviteAsync()
        {
            try
            {
                var cripto = iCryptographyFactory.CreateTokenCryptography();
                var token = $"cpf={request.Cpf}:clienteId={request.ClienteId}";
                var tokenCripted = cripto.GenerateToken(new JwtConfigurationSettings() { value = token }).Result;
                var mongoDb = iMongoDbFactory.Create("TipoEmail");
                var mailText = mongoDb.Get(Builders<TipoEmail>.Filter.Where(o => o.Type == "SendInvite"), cancellationToken).Result.Text;
                mailText.Replace("{{SEU_TOKEN}}", tokenCripted);
                var mail = iMailsFactory.Create();
                _ = mail.SendMail(new Mails.MailsSettings.MailsSettings()
                {
                    Provider = "Hotmail",
                    Subject = "New Token invite",
                    BodyHtml = true,
                    Body = mailText,
                    ToEmail = request.Email
                });
                return await Task.FromResult(new UsuarioSendInviteResponse() { Send = true });
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System)).GenerateLog(ex);
                return await Task.FromResult(new UsuarioSendInviteResponse() { Send = false });
            }
        }
    }
}
