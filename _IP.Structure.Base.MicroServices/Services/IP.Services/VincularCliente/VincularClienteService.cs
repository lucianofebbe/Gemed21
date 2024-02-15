using IP.Application.Comands.Requests.Services;
using IP.Application.Comands.Responses.Services;
using IP.RabbitMQ.RequestReply;

namespace IP.Services.VincularCliente
{
    public class VincularClienteService<Request, Response>:
        IVincularClienteService<Request, Response>
        where Request : VincularClienteRequestService
        where Response : VincularClienteResponseService
    {
        private IRabbitMQRequestReply<Request, Response> IRequestReply;

        public VincularClienteService(
            IRabbitMQRequestReply<Request, Response> iRequestReply)
        {
            IRequestReply = iRequestReply;
        }


        public async Task Add(RabbitMQ.RabbitMQSettings.Settings<Request, Response> settings)
        {
            IRequestReply.Send(settings);
        }
    }
}
