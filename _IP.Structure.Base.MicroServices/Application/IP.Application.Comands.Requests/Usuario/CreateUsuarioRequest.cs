using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IP.Application.Comands.Requests.Usuario
{
    public class CreateUsuarioRequest : BaseRequest, IRequest<CreateUsuarioResponse>
    {
        [Required(ErrorMessage = "preenche campos")]
        [MaxLength(11)]
        public string Cpf { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeExibicao { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public byte[]? Foto { get; set; }
    }
}
