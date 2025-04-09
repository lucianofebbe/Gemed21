using IP.BaseDomains;

namespace IP.Domain
{
    public class AuthorizationResponse : BaseResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
