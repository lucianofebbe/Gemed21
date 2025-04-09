using IP.BaseDomains;

namespace IP.Application.Comands.Responses.Usuario
{
    public class UsuarioGetByCPFResponse : BaseResponse
    {
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
