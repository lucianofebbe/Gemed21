using IP.Application.Comands.Requests.Cliente;
using IP.Application.Comands.Requests.Usuario;
using IP.Application.Comands.Responses.Cliente;
using IP.Application.Comands.Responses.Usuario;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP.Application.Handlers.Cliente
{
    public class ClienteHandler : IRequestHandler<ClienteRequest, ClienteResponse>
    {
        public Task<ClienteResponse> Handle(ClienteRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
