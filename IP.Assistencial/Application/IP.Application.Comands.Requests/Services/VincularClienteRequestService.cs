using IP.Application.Comands.Responses.Services;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Services
{
    public class VincularClienteRequestService : BaseRequest, IRequest<VincularClienteResponseService>
    {
        public Guid Identificador { get; set; }
    }
}
