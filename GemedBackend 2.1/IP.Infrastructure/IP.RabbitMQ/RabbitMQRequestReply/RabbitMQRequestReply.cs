using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.MongoDb.MongoDbConfig;
using IP.RabbitMQ.RabbitMQSettings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IP.RabbitMQ.RequestReply
{
    public class RabbitMQRequestReply<Request, Response> : IRabbitMQRequestReply<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        private readonly IMapperFactory<BaseDomains.BaseDomain, Request, Response> generateMapper;
        private readonly ILogsFactory logsFactory;

        public RabbitMQRequestReply(ILogsFactory iGenerateLogs,
            IMapperFactory<BaseDomains.BaseDomain, Request, Response> generateMapper)
        {
            this.generateMapper = generateMapper;
            this.logsFactory = logsFactory;
        }

        public async Task Send(RabbitMQSettings<Request, Response> settings)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = settings.HostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    // Declara uma fila temporária para receber a resposta
                    var replyQueueName = channel.QueueDeclare().QueueName;

                    var props = channel.CreateBasicProperties();
                    props.ReplyTo = replyQueueName;

                    string mensagem = generateMapper.Create().RequestToJson(settings.Requests).Result;
                    var body = Encoding.UTF8.GetBytes(mensagem);

                    channel.BasicPublish(exchange: settings.Exchange, routingKey: settings.RoutingKey, basicProperties: props, body: body);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var response = Encoding.UTF8.GetString(ea.Body.ToArray());
                        settings.Callback(generateMapper.Create().JsonToResponseList(response).Result);
                    };

                    channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);
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
                var factory = new ConnectionFactory() { HostName = settings.HostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(settings.QueueName, false, settings.Exclusive, settings.AutoDelete, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var mensagem = Encoding.UTF8.GetString(body);

                        var props = channel.CreateBasicProperties();
                        props.CorrelationId = ea.BasicProperties.CorrelationId;

                        var replyProps = channel.CreateBasicProperties();
                        replyProps.CorrelationId = ea.BasicProperties.CorrelationId;

                        var responseBytes = Encoding.UTF8.GetBytes(mensagem);
                        channel.BasicPublish(exchange: settings.Exchange, routingKey: ea.BasicProperties.ReplyTo, basicProperties: replyProps, body: responseBytes);
                    };

                    channel.BasicConsume(queue: settings.QueueName, autoAck: true, consumer: consumer);
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
