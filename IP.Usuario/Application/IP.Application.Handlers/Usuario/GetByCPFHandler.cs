using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Services.Usuario.Request;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByCPFHandler : IRequestHandler<UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>
    {
        private readonly IServicesUsuarioRequest iServices;
        private readonly Logs.Logs.Logs iLogsFactory;

        public GetByCPFHandler(
            IServicesUsuarioRequest iServices,
            ILogsFactory iLogsFactory)
        {
            this.iServices = iServices;
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public Task<UsuarioGetByCPFResponse> Handle(UsuarioGetByCPFRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return iServices.GetByCPFAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
