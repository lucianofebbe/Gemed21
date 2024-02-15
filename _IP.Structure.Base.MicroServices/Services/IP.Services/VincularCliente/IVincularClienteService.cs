
namespace IP.Services.VincularCliente
{
    public interface IVincularClienteService<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        Task Add(RabbitMQ.RabbitMQSettings.Settings<Request, Response> settings);
    }
}
