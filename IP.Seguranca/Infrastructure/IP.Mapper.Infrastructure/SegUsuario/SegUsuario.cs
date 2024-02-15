using IP.BaseDomains;
using IP.Mapper.Infrastructure.SegEmpresa;

namespace IP.Mapper.Infrastructure.SegUsuario
{
    public class SegUsuario<Domain, Request, Response> :
        GenerateMapper<Domain, Request, Response>,
        ISegEmpresa<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
    }
}
