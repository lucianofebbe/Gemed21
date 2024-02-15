using IP.BaseDomains;
using IP.Domain;

namespace IP.Application.Comands.Responses.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public SegUsuario Usuario { get; set; }
        public SegEmpresa Empresa { get; set; }
    }
}
