using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers
{
    public class CreateHandler : IRequestHandler<UsuarioCreateRequest, UsuarioCreateResponse>
    {
        private readonly IServicesFactory iService;
        public CreateHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<UsuarioCreateResponse> Handle(UsuarioCreateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.CreateUsuarioAsync();
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
