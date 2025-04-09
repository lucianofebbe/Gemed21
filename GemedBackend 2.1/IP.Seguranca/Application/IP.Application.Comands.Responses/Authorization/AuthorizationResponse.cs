using IP.BaseDomains;

namespace IP.Application.Comands.Responses.Authorization
{
    public class AuthorizationResponse : BaseResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
