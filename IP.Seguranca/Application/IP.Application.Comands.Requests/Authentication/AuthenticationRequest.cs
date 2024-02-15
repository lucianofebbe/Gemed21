using IP.Application.Comands.Responses.Authentication;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Authentication
{
    public class AuthenticationRequest : BaseRequest, IRequest<AuthenticationResponse>
    {
        public string Cliente { get; set; }
        public string Sistema { get; set; }
        public Guid Identificador { get; set; }
    }
}
