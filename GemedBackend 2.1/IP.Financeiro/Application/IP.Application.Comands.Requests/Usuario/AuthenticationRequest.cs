using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Usuario
{
    public class AuthenticationRequest : BaseRequest, IRequest<AuthenticationResponse>
    {
        public string Cpf { get; set; }
        public string Senha { get; set; }
    }
}
