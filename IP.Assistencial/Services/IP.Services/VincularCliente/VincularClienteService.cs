using IP.Application.Comands.Requests.Services;
using IP.Application.Comands.Responses.Services;
using IP.RabbitMQ.RabbitMQConfig;
using IP.RabbitMQ.RequestReply;

namespace IP.Services.VincularCliente
{
    public class VincularClienteService<Request, Response>:
        IVincularClienteService<Request, Response>
        where Request : VincularClienteRequestService
        where Response : VincularClienteResponseService
    {
        private IRequestReply<Request, Response> IRequestReply;

        public VincularClienteService(
            IRequestReply<Request, Response> iRequestReply)
        {
            IRequestReply = iRequestReply;
        }


        public async Task Add(MQConfiguration<Request, Response> config)
        {
            IRequestReply.Send(config);
        }
    }
}
