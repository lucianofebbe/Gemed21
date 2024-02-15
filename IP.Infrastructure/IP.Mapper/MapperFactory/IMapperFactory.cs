using IP.BaseDomains;
using IP.Mapper.Mapper;

namespace IP.Mapper.MapperFactory
{
    public interface IMapperFactory<Domain, Request, Response>
     where Domain : BaseDomain
     where Request : BaseRequest
     where Response : BaseResponse
    {
        Mapper<Domain, Request, Response> Create();
    }
}
