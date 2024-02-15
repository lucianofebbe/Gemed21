using IP.Application.Comands.Responses.Cliente;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Cliente
{
    public class ClienteGetByIdRequest : BaseRequest, IRequest<ClienteGetByIdResponse>
    {
    }
}
