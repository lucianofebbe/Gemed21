using IP.Application.Comands.Responses;

namespace IP.Services.Requests
{
    public interface IRequestService
    {
        Task<Response> RequestAsync();
    }
}
