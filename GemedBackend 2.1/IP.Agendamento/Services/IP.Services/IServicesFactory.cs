using IP.Application.Comands.Requests.Usuario;
using IP.Logs.LogsFactory;
using IP.Services.Usuario.Authentication;
using IP.Services.Usuario.Create;
using IP.Services.Usuario.GetByCPF;
using IP.Services.Usuario.GetById;
using IP.Services.Usuario.SendInvite;
using IP.Services.Usuario.VincularClinica;

namespace IP.Services
{
    public interface IServicesFactory
    {
        Task<AuthenticationService> Create(AuthenticationRequest request, CancellationToken cancellationToken);
        Task<CreateService> Create(UsuarioCreateRequest request, CancellationToken cancellationToken);
        Task<GetByCPFService> Create(UsuarioGetByCPFRequest request, CancellationToken cancellationToken);
        Task<GetByIdService> Create(UsuarioGetByIdRequest request, CancellationToken cancellationToken);
        Task<SendInviteService> Create(UsuarioSendInviteRequest request, CancellationToken cancellationToken);
        Task<VincularClinicaService> Create(UsuarioVincularClinicaRequest request, CancellationToken cancellationToken);
        Task<LogsFactory> CreateLogs();
    }
}
