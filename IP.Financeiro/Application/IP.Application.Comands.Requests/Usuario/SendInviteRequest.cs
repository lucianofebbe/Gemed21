using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Usuario
{
    public class SendInviteRequest : BaseRequest, IRequest<SendInviteResponse>
    {
        public string Cpf { get; set; }
        public int ClienteId { get; set; }
        public string Email { get; set; }
    }
}
