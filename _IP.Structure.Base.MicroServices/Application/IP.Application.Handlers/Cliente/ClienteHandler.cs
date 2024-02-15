using IP.Application.Comands.Requests.Cliente;
using IP.Application.Comands.Responses.Cliente;
using IP.Logs.GeneratorFactory;
using IP.Logs.GeneratorLog;
using IP.Logs.GeneratorLogSettings;
using MediatR;

namespace IP.Application.Handlers.Cliente
{
    public class ClienteHandler : IRequestHandler<ClienteRequest, ClienteResponse>
    {
        GeneratorLog log;

        public ClienteHandler(IGeneratorLogFactory log)
        {
            this.log = log.Create(new LogSettings() { SendMail = true });
        }

        public Task<ClienteResponse> Handle(ClienteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                log.GenerateLog(ex);
                throw new ApplicationException("", ex);
            }
        }
    }
}
