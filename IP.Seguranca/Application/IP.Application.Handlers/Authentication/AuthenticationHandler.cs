using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.CacheDomain;
using IP.CacheRepository.Infrastructure.TipoEmpresas;
using IP.Domain;
using IP.MongoDb;
using IP.Repository.Infrastructure.Repositories.SegEmpresa;
using MediatR;

namespace IP.Application.Handlers.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        ISegEmpresaRepository<SegEmpresa> IRepositories;
        ITipoEmpresas<TipoEmpresas, MongoDbConfig> ICacheRepositories;

        public AuthenticationHandler(
            ISegEmpresaRepository<Domain.SegEmpresa> ISegEmpresaRepositories,
            ITipoEmpresas<TipoEmpresas, MongoDbConfig> ITipoEmpresaRepositories)
        {
            IRepositories = ISegEmpresaRepositories;
            ICacheRepositories = ITipoEmpresaRepositories;
        }

        public Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            var config = new MongoDbConfig()
            {
                ConnectionString = "mongodb://Gemed2022:Gemed2022Teste@localhost:27018/?authSource=admin",
                Database = "DbCache",
                Collection = "TipoEmpresas"
            };

            var result = ICacheRepositories.GetAll(Task.FromResult(config), null, cancellationToken).Result;
            return Task.FromResult(new AuthenticationResponse());
        }
    }
}
