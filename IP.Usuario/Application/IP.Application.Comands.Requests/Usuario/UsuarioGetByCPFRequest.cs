using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IP.Application.Comands.Requests.Usuario
{
    public class UsuarioGetByCPFRequest : BaseRequest, IRequest<UsuarioGetByCPFResponse>
    {
        [Required(ErrorMessage = "Invalid Cpf")]
        [MaxLength(11)]
        public string Cpf { get; set; }
    }
}
