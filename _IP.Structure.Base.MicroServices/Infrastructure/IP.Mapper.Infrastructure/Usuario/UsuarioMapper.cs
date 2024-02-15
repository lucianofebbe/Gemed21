using IP.BaseDomains;

namespace IP.Mapper.Infrastructure.Usuario
{
    public class UsuarioMapper<Domain, Request, Response> :
        GenerateMapper<Domain, Request, Response>,
        IUsuarioMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {

    }
}
