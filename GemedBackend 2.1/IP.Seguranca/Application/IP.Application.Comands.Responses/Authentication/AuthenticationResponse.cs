using IP.BaseDomains;

namespace IP.Application.Comands.Responses.Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
