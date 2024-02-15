using IP.BaseDomains;

namespace IP.Application.Comands.Responses.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Cpf { get; set; }
        public string NomeExibicao { get; set; }
        public string TokenAutenticacao { get; set; }
    }
}
