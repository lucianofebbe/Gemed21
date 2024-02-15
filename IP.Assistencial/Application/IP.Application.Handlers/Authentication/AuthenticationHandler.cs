using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Cryptography.PBKDF2Cryptography;
using IP.Cryptography.TokenCryptography;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.Usuarios;
using MediatR;

namespace IP.Application.Handlers.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        IUsuarioRepository<Domain.Usuario> IRepositories;
        IUsuarioMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse> IMapper;
        IPBKDF2Cryptography ICryptography;
        ITokenCryptography IToken;

        public AuthenticationHandler(
            IUsuarioRepository<Domain.Usuario> iUsuarioRepositories,
            IUsuarioMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse> iGenerateMapper,
            IPBKDF2Cryptography IPbkdf2Cryptography, ITokenCryptography ITokenCryptography)
        {
            IRepositories = iUsuarioRepositories;
            IMapper = iGenerateMapper;
            ICryptography = IPbkdf2Cryptography;
            IToken = ITokenCryptography;
        }

        public Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var resultUsuario = IRepositories.Get(o => o.Cpf == request.Cpf, cancellationToken).Result;
            var resultCript = ICryptography.CompareHash(Task.FromResult(request.Senha), Task.FromResult(resultUsuario.Salt)).Result;
            if(!resultCript)
                throw new UnauthorizedAccessException();

            var response = IMapper.DomainToResponse(Task.FromResult(resultUsuario)).Result;
            response.TokenAutenticacao = IToken.GenerateToken(Task.FromResult(response.Id)).Result;
            return Task.FromResult(response);
        }
    }
}
