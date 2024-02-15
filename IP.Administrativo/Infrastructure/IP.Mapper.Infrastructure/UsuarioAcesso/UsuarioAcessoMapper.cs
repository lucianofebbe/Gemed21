using IP.BaseDomains;

namespace IP.Mapper.Infrastructure.UsuarioAcesso
{
    public class UsuarioAcessoMapper<Domain, Request, Response>
        : GenerateMapper<Domain, Request, Response>,
        IUsuarioAcessoMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {

    }
}
