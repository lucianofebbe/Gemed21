using Azure.Core;
using IP.Application.Comands.Requests.Menu;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Menu;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Mapper.MapperFactory;
using IP.MongoDb.MongoDbConfig;
using IP.Repository.Infrastructure.Contexts;
using IP.UnitEntityFramework.UnitEntityFramework.RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IP.Services.Usuario.Menu
{
    public class MenuService : IMenuService
    {
        private readonly MenuRequest request;
        private readonly CancellationToken cancellationToken;
        private readonly ILogsFactory iLogsFactory;
        private readonly IRepositoryFactory<Domains.Domain.Menu> iRepositoryFactory;
        private readonly IMapperFactory<Domains.Domain.Menu, MenuRequest, MenuResponse> iMapper;

        public MenuService(
            MenuRequest request,
            CancellationToken cancellationToken,
            ILogsFactory iLogsFactory,
            IRepositoryFactory<Domains.Domain.Menu> iRepositoryFactory,
            IMapperFactory<Domains.Domain.Menu, MenuRequest, MenuResponse> iMapper) 
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iLogsFactory = iLogsFactory;
            this.iRepositoryFactory = iRepositoryFactory;
            this.iMapper = iMapper;
        }

        public async Task<MenuResponse> GetMenuAsync()
        {
            try
            {
                var repositorio = iRepositoryFactory.CreateRequests(new ContextDefault(request.ConnectionString, request.ProviderName), cancellationToken);
                var result = repositorio.Get(null, cancellationToken).Result;
                var mapper = iMapper.Create();
                var responseMapper = mapper.DomainToResponse(result);
                return await responseMapper;

            }
            catch (Exception ex)
            {
                _ = iLogsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
