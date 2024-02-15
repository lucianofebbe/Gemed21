using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Services.Usuario.Commands;
using MediatR;

namespace IP.Application.Handlers
{
    public class CreateHandler : IRequestHandler<UsuarioCreateRequest, UsuarioCreateResponse>
    {
        private readonly IServicesUsuarioCommands iServices;
        private readonly Logs.Logs.Logs iLogsFactory;
        public CreateHandler(IServicesUsuarioCommands iServices,
            ILogsFactory iLogsFactory)
        {
            this.iServices = iServices;
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public Task<UsuarioCreateResponse> Handle(UsuarioCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return iServices.CreateUsuarioAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
