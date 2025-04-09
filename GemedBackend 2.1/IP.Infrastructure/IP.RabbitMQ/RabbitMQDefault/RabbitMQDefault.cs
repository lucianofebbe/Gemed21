using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.MongoDb.MongoDbConfig;
using IP.RabbitMQ.RabbitMQSettings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IP.RabbitMQ.Default
{
    public class RabbitMQDefault<Request, Response> : IRabbitMQDefault<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        private readonly IMapperFactory<BaseDomains.BaseDomain, Request, Response> generateMapper;
        private readonly ILogsFactory logsFactory;

        public RabbitMQDefault(ILogsFactory logsFactory,
            IMapperFactory<BaseDomains.BaseDomain, Request, Response> generateMapper)
        {
            this.generateMapper = generateMapper;
            this.logsFactory = logsFactory;
        }

        public async Task Send(RabbitMQSettings<Request, Response> settings)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = settings.HostName }; // Altere para o endereço do seu servidor RabbitMQ, se necessário
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: settings.QueueName,
                                         durable: false,
                                         exclusive: settings.Exclusive,
                                         autoDelete: settings.AutoDelete,
                                         arguments: null);

                    string mensagem = generateMapper.Create().RequestToJson(settings.Requests).Result;
                    var body = Encoding.UTF8.GetBytes(mensagem);

                    channel.BasicPublish(exchange: settings.Exchange,
                                         routingKey: settings.RoutingKey,
                                         basicProperties: null,
                                         body: body);

                }
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task Receive(RabbitMQSettings<Request, Response> settings)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = settings.HostName }; // Altere para o endereço do seu servidor RabbitMQ, se necessário
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: settings.QueueName,
                                         durable: false,
                                         exclusive: settings.Exclusive,
                                         autoDelete: settings.AutoDelete,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var mensagem = Encoding.UTF8.GetString(body);

                        settings.Callback(generateMapper.Create().JsonToResponseList(mensagem).Result);
                    };

                    channel.BasicConsume(queue: settings.QueueName,
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
