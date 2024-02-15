using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Mapper.Mapper;

namespace IP.Mapper.Infrastructure.Authentication
{
    public class AuthenticationMapper :
        Mapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse>,
        IAuthenticationMapper
    {
    }
}
