using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Usuario;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Services.Usuario.Request;
using MediatR;

namespace IP.Application.Handlers.Usuario
{
    public class GetByIdHandler : IRequestHandler<UsuarioGetByIdRequest, UsuarioGetByIdResponse>
    {
        private readonly IServicesUsuarioRequest iServices;
        private readonly Logs.Logs.Logs iLogsFactory;

        public GetByIdHandler(
            IServicesUsuarioRequest iServices,
            ILogsFactory iLogsFactory)
        {
            this.iServices = iServices;
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public Task<UsuarioGetByIdResponse> Handle(UsuarioGetByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return iServices.GetByIdAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                _ = iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
