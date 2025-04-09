using IP.BaseDomains;

namespace IP.Domain
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Usuario { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Imagem { get; set; }
        public string Cpf { get; set; }
        public int TentativasLogin { get; set; }

        public AuthorizationResponse Authorization { get; set; }
    }
}
