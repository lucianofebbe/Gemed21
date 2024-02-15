using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Cryptography.PBKDF2Cryptography;
using IP.Cryptography.TokenCryptography;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit;
using MediatR;

namespace IP.Application.Handlers.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly UnitOfWork<Domain.Usuario> iUsuarioUnit;
        private readonly UsuarioMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse> iMapper;
        private readonly PBKDF2Cryptography iCryptography;
        private readonly TokenCryptography iToken;
        private readonly GeneratorLog log;

        public AuthenticationHandler(
            IRepositoryFactory<Domain.Usuario> iUsuarioFactory,
            UsuarioMapper<Domain.Usuario, AuthenticationRequest, AuthenticationResponse> iGenerateMapper,
            PBKDF2Cryptography iPbkdf2Cryptography,
            TokenCryptography iTokenCryptography,
            IGeneratorLogFactory log)
        {
            iUsuarioUnit = iUsuarioFactory.Create();
            iMapper = iGenerateMapper;
            iCryptography = iPbkdf2Cryptography;
            iToken = iTokenCryptography;
            this.log = log.Create(new LogSettings() { SendMail = true });
        }

        public Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var resultUsuario = iUsuarioUnit.Get(o => o.Cpf == request.Cpf, cancellationToken).Result;
                var resultCript = iCryptography.CompareHash(Task.FromResult(request.Senha), Task.FromResult(resultUsuario.Salt)).Result;
                if (!resultCript)
                    throw new UnauthorizedAccessException();

                var response = iMapper.DomainToResponse(Task.FromResult(resultUsuario)).Result;
                response.TokenAutenticacao = iToken.GenerateToken(Task.FromResult(response.Id)).Result;
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                log.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
