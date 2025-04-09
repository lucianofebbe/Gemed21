using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.Mapper.MapperFactory;
using IP.RabbitMQ.Default;
using IP.RabbitMQ.RequestReply;

namespace IP.RabbitMQ.RabbitMQFactory
{
    public class RabbitMQFactory<Request, Response> : IRabbitMQFactory<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        public RabbitMQDefault<Request, Response> CreateDefault()
        {
            return new RabbitMQDefault<Request, Response>(new LogsFactory(), CreateMapper());
        }

        public RabbitMQRequestReply<Request, Response> CreateRequestReply()
        {
            return new RabbitMQRequestReply<Request, Response>(new LogsFactory(),CreateMapper());
        }

        private MapperFactory<BaseDomain, Request, Response> CreateMapper()
        {
            return new MapperFactory<BaseDomain, Request, Response>();
        }
    }
}
