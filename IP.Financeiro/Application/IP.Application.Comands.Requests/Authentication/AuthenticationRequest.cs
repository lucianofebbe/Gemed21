using IP.Application.Comands.Responses.Authentication;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Authentication
{
    public class AuthenticationRequest : BaseRequest, IRequest<AuthenticationResponse>
    {
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }
}
