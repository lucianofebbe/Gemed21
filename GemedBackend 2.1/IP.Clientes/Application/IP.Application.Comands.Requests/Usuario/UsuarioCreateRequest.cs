using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IP.Application.Comands.Requests.Usuario
{
    public class UsuarioCreateRequest : BaseRequest, IRequest<UsuarioCreateResponse>
    {
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Invalid CPF format.")]
        public string Cpf { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        [Required(ErrorMessage = "Invalid name")]
        [MaxLength(100, ErrorMessage = "Max characters permited")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Invalid name")]
        [MaxLength(100, ErrorMessage = "Max characters permited")]
        public string NomeExibicao { get; set; }
        public DateTime DataNascimento { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Mail")]
        public string Email { get; set; }

        [Phone(ErrorMessage ="Invalid Phone")]
        public string Telefone { get; set; }
        public byte[]? Foto { get; set; }
    }
}
