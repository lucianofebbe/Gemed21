using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByIdHandler : IRequestHandler<UsuarioGetByIdRequest, UsuarioGetByIdResponse>
    {
        private readonly IServicesFactory iService;

        public GetByIdHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<UsuarioGetByIdResponse> Handle(UsuarioGetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.GetByIdAsync();
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
