using IP.BaseDomains;

namespace IP.Application.Comands.Responses.Cliente
{
    public class ClienteResponse : BaseResponse
    {
        public string Fantasia { get; set; }
        public string RazaoSocial { get; set; }
        public byte[]? Logotipo { get; set; }
    }
}
