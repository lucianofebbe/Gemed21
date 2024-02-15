using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using MediatR;
using System.Text;
using static IP.Mails.ISendMails;

namespace IP.Application.Handlers.Usuario
{
    public class SendInviteHandler : IRequestHandler<SendInviteRequest, SendInviteResponse>
    {
        IEmailSend IMails;
        public SendInviteHandler(IEmailSend iEmailSend)
        {
            IMails = iEmailSend;
        }
        public Task<SendInviteResponse> Handle(SendInviteRequest request, CancellationToken cancellationToken)
        {
            var tokenValue = $"cpf={request.Cpf}:clienteId={request.ClienteId}";
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenValue));
            var settings = IMails.GetConfigurationsToMail("").Result;
            IMails.SendMail(settings);
            return Task.FromResult(new SendInviteResponse() { Send = true });
        }
    }
}
