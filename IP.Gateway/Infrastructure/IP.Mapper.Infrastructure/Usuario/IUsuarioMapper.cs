using IP.BaseDomains;

namespace IP.Mapper.Infrastructure.Usuario
{
    public interface IUsuarioMapper<Domain, Request, Response> : IGenerateMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
    }
}
