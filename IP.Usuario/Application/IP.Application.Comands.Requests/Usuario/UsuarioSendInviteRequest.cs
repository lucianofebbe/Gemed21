using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IP.Application.Comands.Requests.Usuario
{
    public class UsuarioSendInviteRequest : BaseRequest, IRequest<UsuarioSendInviteResponse>
    {
        [Required(ErrorMessage = "Invalid Cpf")]
        [MaxLength(11)]
        public string Cpf { get; set; }
        public int ClienteId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Mail")]
        public string Email { get; set; }
    }
}
