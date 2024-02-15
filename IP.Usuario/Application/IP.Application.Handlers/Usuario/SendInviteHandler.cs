using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Services.Usuario.Request;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class SendInviteHandler : IRequestHandler<UsuarioSendInviteRequest, UsuarioSendInviteResponse>
    {

        private readonly IServicesUsuarioRequest iServices;
        private readonly Logs.Logs.Logs iLogsFactory;

        public SendInviteHandler(
            IServicesUsuarioRequest iServices,
            ILogsFactory iLogsFactory)
        {
            this.iServices = iServices;
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public Task<UsuarioSendInviteResponse> Handle(UsuarioSendInviteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return iServices.SendInviteAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
