using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class SendInviteHandler : IRequestHandler<UsuarioSendInviteRequest, UsuarioSendInviteResponse>
    {
        private readonly IServicesFactory iService;

        public SendInviteHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<UsuarioSendInviteResponse> Handle(UsuarioSendInviteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.SendInviteAsync();
            }
            catch (Exception ex)
            {
                var service = iService.CreateLogs().Result;
                _ = service.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
