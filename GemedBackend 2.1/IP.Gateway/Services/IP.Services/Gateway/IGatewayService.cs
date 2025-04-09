using IP.Application.Comands.Responses;

namespace IP.Services.Gateway
{
    public interface IGatewayService
    {
        Task<Response> SendRequestAsync();

    }
}
