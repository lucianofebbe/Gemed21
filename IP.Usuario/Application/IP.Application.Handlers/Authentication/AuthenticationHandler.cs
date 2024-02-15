using IP.Application.Comands.Requests.Authentication;
using IP.Application.Comands.Responses.Authentication;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.Services.Usuario.Request;
using MediatR;

namespace IP.Application.Handlers.Authentication
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IServicesUsuarioRequest iServices;
        private readonly Logs.Logs.Logs iLogsFactory;

        public AuthenticationHandler(
            IServicesUsuarioRequest iServices,
            ILogsFactory iLogsFactory)
        {
            this.iServices = iServices;
            this.iLogsFactory = iLogsFactory.Create(new LogsSettings() { SendMail = true });
        }

        public Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                iLogsFactory.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
