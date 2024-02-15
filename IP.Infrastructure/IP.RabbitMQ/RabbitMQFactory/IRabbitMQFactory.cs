using IP.RabbitMQ.Default;
using IP.RabbitMQ.RequestReply;

namespace IP.RabbitMQ.RabbitMQFactory
{
    public interface IRabbitMQFactory<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        RabbitMQDefault<Request, Response> CreateDefault();
        RabbitMQRequestReply<Request, Response> CreateRequestReply();
    }
}
