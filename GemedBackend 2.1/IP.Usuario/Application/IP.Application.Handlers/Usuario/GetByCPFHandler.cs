using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByCPFHandler : IRequestHandler<UsuarioGetByCPFRequest, UsuarioGetByCPFResponse>
    {
        private readonly IServicesFactory iService;

        public GetByCPFHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<UsuarioGetByCPFResponse> Handle(UsuarioGetByCPFRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request,cancellationToken).Result;
                return await service.GetByCPFAsync();
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
