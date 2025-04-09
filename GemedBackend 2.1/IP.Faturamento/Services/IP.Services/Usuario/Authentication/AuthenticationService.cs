using AutoMapper;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Cryptography.CryptographyFactory;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.Repository.Infrastructure.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory;

namespace IP.Services.Usuario.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly ICryptographyFactory iCryptographyFactory;
        private readonly IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory;
        private readonly IMapperFactory<Domains.Domain.Usuario, AuthenticationRequest, AuthenticationResponse> iMapperFactory;
        private readonly ILogsFactory iLogsFactory;

        public AuthenticationService(
            AuthenticationRequest request,
            CancellationToken cancellationToken,
            ICryptographyFactory iCryptographyFactory,
            IRepositoryFactory<Domains.Domain.Usuario> iRepositoryFactory,
            IMapperFactory<Domains.Domain.Usuario, AuthenticationRequest, AuthenticationResponse> iMapperFactory,
            ILogsFactory iLogsFactory)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iCryptographyFactory = iCryptographyFactory;
            this.iRepositoryFactory = iRepositoryFactory;
            this.iMapperFactory = iMapperFactory;
            this.iLogsFactory = iLogsFactory;
        }

        public async Task<AuthenticationResponse> AuthenticationAsync()
        {
            try
            {
                var result = new AuthenticationResponse();
                var cripto = iCryptographyFactory.CreatePBKDF2Cryptography();
                var repoRequests = iRepositoryFactory.CreateRequests(new ContextDefault(request.ConnectionString, request.ProviderName), cancellationToken);
                var repoCommands = iRepositoryFactory.CreateCommands(new ContextDefault(request.ConnectionString, request.ProviderName), cancellationToken);

                var usuario = await repoRequests.Get(o => o.Cpf == request.Cpf, cancellationToken);
                if (usuario == null)
                {
                    result.Message = "Usuário nao encontrado";
                    return result;
                }
                else if (usuario.Status != "A")
                {
                    _ = iLogsFactory
                        .Create(new LogsSettings(false, TypeLogs.Authentication))
                        .GenerateLog("Usuário: " + usuario.Cpf + " Está temporáriamente bloqueado.");

                    result.Message = "Usuário Está temporáriamente bloqueado.";
                    return result;
                }
                else if (!cripto.CompareHash(request.Senha, usuario.Hash, usuario.Salt).Result)
                {
                    if (usuario.TentativasLogin < 3)
                    {
                        usuario.TentativasLogin++;
                        usuario.DataHoraBloqueio = DateTime.UtcNow;
                        _ = repoCommands.Update(usuario, cancellationToken);
                    }
                    else
                    {
                        usuario.Status = "B";
                        _ = repoCommands.Update(usuario, cancellationToken);
                    }

                    result.Message = "Senha inválida.";
                    return result;
                }
                else
                {
                    usuario.Status = "A";
                    usuario.TentativasLogin = 0;
                    usuario.DataHoraBloqueio = null;
                    _ = repoCommands.Update(usuario, cancellationToken);

                    var configMapper = new MapperConfiguration(config =>
                    {
                        config.CreateMap<Domains.Domain.Usuario, AuthenticationResponse>()
                        .ForMember(dest => dest.Imagem, opt => opt.MapFrom(src => src.CaminhoFoto))
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUsuario));
                    });

                    var mapper = iMapperFactory.Create(configMapper);
                    result = mapper.DomainToResponse(usuario).Result;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System)).GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
