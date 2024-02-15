using IP.BaseDomains;

namespace IP.Mapper.Infrastructure.SegEmpresa
{
    public class SegEmpresa<Domain, Request, Response> :
        GenerateMapper<Domain, Request, Response>,
        ISegEmpresa<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
    }
}
