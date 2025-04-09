using IP.Application.Comands.Requests.Menu;
using IP.Application.Comands.Responses.Menu;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers.Menu
{
    public class MenuHandler : IRequestHandler<MenuRequest, MenuResponse>
    {
        private readonly IServicesFactory iService;
        public MenuHandler(
            IServicesFactory iService)
        {
            this.iService = iService;
        }

        public async Task<MenuResponse> Handle(MenuRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iService.Create(request, cancellationToken).Result;
                return await service.GetMenuAsync();
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
