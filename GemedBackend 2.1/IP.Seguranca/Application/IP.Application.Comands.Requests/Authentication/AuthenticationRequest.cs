using IP.Application.Comands.Responses.Authentication;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Authentication
{
    public class AuthenticationRequest : BaseRequest, IRequest<AuthenticationResponse>
    {
    }
}
