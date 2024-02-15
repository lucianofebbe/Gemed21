using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IP.Application.Comands.Requests.Usuario
{
    public class GetByCPFRequest : BaseRequest, IRequest<GetByCPFResponse>
    {
        [Required(ErrorMessage = "CPF Inválido")]
        [MaxLength(11)]
        public string Cpf { get; set; }
    }
}
