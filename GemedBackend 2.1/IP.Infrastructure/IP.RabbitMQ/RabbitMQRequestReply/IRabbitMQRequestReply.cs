using IP.RabbitMQ.RabbitMQSettings;

namespace IP.RabbitMQ.RequestReply
{
    public interface IRabbitMQRequestReply<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        Task Send(RabbitMQSettings<Request, Response> settings);
        Task Receive(RabbitMQSettings<Request, Response> settings);
    }
}
