using IP.Application.Comands.Requests.Services;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Services;
using IP.Application.Comands.Responses.Usuario;
using IP.Domain;
using IP.Mapper.Infrastructure.Usuario;
using IP.RabbitMQ.RabbitMQConfig;
using IP.Repository.Infrastructure.UsuarioAcessos;
using IP.Services.VincularCliente;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class VincularClienteHandlers : IRequestHandler<VincularClienteRequest, VincularClienteResponse>
    {
        IUsuarioAcessoRepository<Domain.UsuarioAcesso> IRepositoryUsuarioAcesso;
        IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService> IVincularClienteService;
        IUsuarioMapper<Domain.Usuario, VincularClienteRequest, VincularClienteResponse> IMapper;


        public VincularClienteHandlers(
            IUsuarioAcessoRepository<Domain.UsuarioAcesso> iRepositoryUsuarioAcesso,
            IVincularClienteService<VincularClienteRequestService, VincularClienteResponseService> iVincularClienteService,
            IUsuarioMapper<Domain.Usuario, VincularClienteRequest, VincularClienteResponse> iMapper
            )
        {
            //IMapper = iMapper;
            IRepositoryUsuarioAcesso = iRepositoryUsuarioAcesso;
            IVincularClienteService = iVincularClienteService;
        }
        public Task<VincularClienteResponse> Handle(VincularClienteRequest request, CancellationToken cancellationToken)
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

            var config = new MQConfiguration<VincularClienteRequestService, VincularClienteResponseService>();
            IVincularClienteService.Add(config);
            //return IMapper.DomainToResponse(resultAddUsuarioAcesso);
            return Task.FromResult(new VincularClienteResponse());
        }
    }
}
