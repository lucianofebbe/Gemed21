using IP.Application.Comands.Requests.Cliente;
using IP.Application.Comands.Responses.Cliente;

namespace IP.Services.Cliente.Request
{
    public interface IServicesClienteRequest
    {
        Task<ClienteGetByIdResponse> GetByIdAsync(ClienteGetByIdRequest request, CancellationToken cancellationToken);
    }
}
