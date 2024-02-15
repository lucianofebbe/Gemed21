using IP.Application.Comands.Requests.Services;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Services;
using IP.Application.Comands.Responses.Usuario;
using IP.Domain;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using IP.Mapper.Infrastructure.Usuario;
using IP.Repository.Infrastructure.RepositoryFactory;
using IP.Repository.Infrastructure.Unit;
using IP.Services.VincularCliente;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class VincularClienteHandlers : IRequestHandler<VincularClienteRequest, VincularClienteResponse>
    {
        IUnitOfWork<Domain.UsuarioAcesso> IRepositoryUsuarioAcesso;
        IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService> IVincularClienteService;
        IUsuarioMapper<Domain.Usuario, VincularClienteRequest, VincularClienteResponse> IMapper;
        GeneratorLog log;

        public VincularClienteHandlers(
            IRepositoryFactory<Domain.UsuarioAcesso> iRepositoryUsuarioAcesso,
            IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService> iVincularClienteService,
            IUsuarioMapper<Domain.Usuario, VincularClienteRequest, VincularClienteResponse> iMapper,
            IGeneratorLogFactory log)
        {
            IMapper = iMapper;
            IRepositoryUsuarioAcesso = iRepositoryUsuarioAcesso.Create();
            IVincularClienteService = iVincularClienteService;
            this.log = log.Create(new LogSettings() { SendMail = true });
        }
        public Task<VincularClienteResponse> Handle(VincularClienteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarioAcesso = new UsuarioAcesso()
                {
                    Id = 0,
                    status = "",
                    usuarioId = "",
                    clienteId = "",
                    identificador = Guid.NewGuid()
                };

                var resultAddUsuarioAcesso = IRepositoryUsuarioAcesso.Add(usuarioAcesso, cancellationToken);
                //var config = new MQConfiguration<VincularClienteRequestService, VincularClienteResponseService>();
                //IVincularClienteService.Add(config);
                //return IMapper.DomainToResponse(resultAddUsuarioAcesso);
                return Task.FromResult(new VincularClienteResponse());
            }
            catch (Exception ex)
            {
                log.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
