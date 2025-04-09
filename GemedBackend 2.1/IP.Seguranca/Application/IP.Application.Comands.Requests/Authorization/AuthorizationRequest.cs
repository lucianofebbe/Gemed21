using IP.Application.Comands.Responses.Authorization;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Authorization
{
    public class AuthorizationRequest : BaseRequest, IRequest<AuthorizationResponse>
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
}
