using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.Services;
using MediatR;

namespace IP.Application.Handlers
{
    public class GatewayHandler : IRequestHandler<Request, Response>
    {
        private readonly IServicesFactory iServices;

        public GatewayHandler(
            IServicesFactory iServices)
        {
            this.iServices = iServices;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                var service = iServices.CreateGatewayService(request, cancellationToken).Result;
                return await service.SendRequestAsync();
            }
            catch (Exception ex)
            {
                var service = iServices.CreateLogs().Result;

                _ = service.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new ApplicationException("", ex);
            }
        }
    }
}
