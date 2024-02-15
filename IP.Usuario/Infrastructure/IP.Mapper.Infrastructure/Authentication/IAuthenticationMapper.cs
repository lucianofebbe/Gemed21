using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Mapper.Mapper;

namespace IP.Mapper.Infrastructure.Authentication
{
    public interface IAuthenticationMapper :
        IMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse>
    {
    }
}
