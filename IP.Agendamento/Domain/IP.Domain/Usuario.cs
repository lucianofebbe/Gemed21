using IP.BaseDomains;
using System.ComponentModel.DataAnnotations;

namespace IP.Domain
{
    public class Usuario : BaseDomain
    {
        [Required]
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
