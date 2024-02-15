using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.DomainMongo;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using IP.Mails.Mails;
using IP.Mails.MailsFactory;
using IP.Mails.MailsSettings;
using IP.MongoDb.MongoDb;
using IP.MongoDb.MongoDbFactory;
using MediatR;
using System.Text;

namespace IP.Application.Handlers.Usuario
{
    public class SendInviteHandler : IRequestHandler<SendInviteRequest, SendInviteResponse>
    {
        SendMails IMails;
        MongoDataBase<TipoEmail> mongoDb;
        GeneratorLog log;
        public SendInviteHandler(MailsFactory iEmailSend, IMongoDbFactory<TipoEmail> mongoDb, IGeneratorLogFactory log)
        {
            IMails = iEmailSend.Create();
            this.mongoDb = mongoDb.Create("TipoEmail");
            this.log = log.Create(new LogSettings() { SendMail = true });
        }
        public Task<SendInviteResponse> Handle(SendInviteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tokenValue = $"cpf={request.Cpf}:clienteId={request.ClienteId}";
                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenValue));

                IMails.SendMail(new MailSettings()
                {
                    Provider = "Hotmail",
                    Body = "token",
                    ToEmail = "lucianofebbedossantos@hotmail.com",

                });

                return Task.FromResult(new SendInviteResponse() { Send = true });
            }
            catch (Exception ex)
            {
                log.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
