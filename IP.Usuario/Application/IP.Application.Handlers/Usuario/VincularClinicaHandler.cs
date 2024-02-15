using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Services.Usuario.Request;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class VincularClinicaHandler : IRequestHandler<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>
    {
        private readonly IServicesUsuarioRequest iServices;
        private readonly Logs.Logs.Logs iLogsFactory;

        public VincularClinicaHandler(
            IServicesUsuarioRequest iServices,
            ILogsFactory iLogsFactory)
        {
            this.iServices = iServices;
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public Task<UsuarioVincularClinicaResponse> Handle(UsuarioVincularClinicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return iServices.VincularClinica(request, cancellationToken);
            }
            catch (Exception ex)
            {
                iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
