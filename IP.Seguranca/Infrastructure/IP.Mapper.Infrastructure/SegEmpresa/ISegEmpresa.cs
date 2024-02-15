using IP.BaseDomains;

namespace IP.Mapper.Infrastructure.SegEmpresa
{
    public interface ISegEmpresa<Domain, Request, Response> : IGenerateMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
    }
}
