
using IP.RabbitMQ.RabbitMQConfig;

namespace IP.Services.VincularCliente
{
    public interface IVincularClienteService<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        Task Add(MQConfiguration<Request, Response> config);
    }
}
