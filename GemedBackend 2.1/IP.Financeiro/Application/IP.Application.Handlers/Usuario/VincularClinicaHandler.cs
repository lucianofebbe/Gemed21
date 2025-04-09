using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class VincularClinicaHandler : IRequestHandler<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>
    {
        private readonly IServicesFactory iService;

        public VincularClinicaHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<UsuarioVincularClinicaResponse> Handle(UsuarioVincularClinicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.VincularClinica();
            }
            catch (Exception ex)
            {
                var service = iService.CreateLogs().Result;
                _ = service.Create(new LogsSettings(false, TypeLogs.System)).GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
