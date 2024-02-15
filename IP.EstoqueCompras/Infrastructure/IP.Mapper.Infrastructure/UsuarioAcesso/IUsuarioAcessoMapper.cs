using IP.BaseDomains;

namespace IP.Mapper.Infrastructure.UsuarioAcesso
{
    public interface IUsuarioAcessoMapper<Domain, Request, Response> : IGenerateMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {

    }
}
