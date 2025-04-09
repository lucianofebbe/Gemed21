using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using IP.RabbitMQ.RabbitMQFactory;
using IP.RabbitMQ.RabbitMQSettings;

namespace IP.Services.Usuario.VincularClinica
{
    public class VincularClinicaService : IVincularClinicaService
    {
        private readonly UsuarioVincularClinicaRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly IRabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse> iRabbitMQFactory;
        private readonly ILogsFactory iLogsFactory;
        public VincularClinicaService(
            UsuarioVincularClinicaRequest request,
            CancellationToken cancellationToken,
            IRabbitMQFactory<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse> iRabbitMQFactory,
            ILogsFactory iLogsFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iRabbitMQFactory = iRabbitMQFactory;
            this.iLogsFactory = iLogsFactory;
        }
        public async Task<UsuarioVincularClinicaResponse> VincularClinica()
        {
            try
            {
                //var cliente = await iServicesClienteRequest.GetByIdAsync(new Application.Comands.Requests.Cliente.ClienteGetByIdRequest() { Id = request.ClienteId }, cancellationToken);

                var listRequest = new List<UsuarioVincularClinicaRequest>
                {
                    request
                };

                var settings = new RabbitMQSettings<UsuarioVincularClinicaRequest, UsuarioVincularClinicaResponse>()
                {
                    HostName = "http://localhost:9092/",
                    Requests = listRequest,
                    Exchange = "Direct_Exchange",
                    AutoDelete = false,
                    Exclusive = true,
                    QueueName = "UsuarioVincularClinica",
                    RoutingKey = "UsuarioVincular"
                };

                var requestReply = iRabbitMQFactory.CreateRequestReply();
                _ = requestReply.Send(settings);
                return new UsuarioVincularClinicaResponse() { };
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                return await Task.FromResult(new UsuarioVincularClinicaResponse());
            }
        }
    }
}
