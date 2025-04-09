using AutoMapper;
using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.Mapper.Mapper;

namespace IP.Mapper.MapperFactory
{
    public class MapperFactory<Domain, Request, Response> : IMapperFactory<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
        public Mapper<Domain, Request, Response> Create()
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain, Request>();
                cfg.CreateMap<Request, Domain>();

                cfg.CreateMap<Domain, Response>();
                cfg.CreateMap<Response, Domain>();
            });

            return new Mapper<Domain, Request, Response>(new LogsFactory(), mapper);
        }

        public Mapper<Domain, Request, Response> Create(MapperConfiguration configuration)
        {
            return new Mapper<Domain, Request, Response>(new LogsFactory(), configuration);
        }
    }
}
