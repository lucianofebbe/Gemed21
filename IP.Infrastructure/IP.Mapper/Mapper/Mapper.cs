using AutoMapper;
using IP.BaseDomains;
using Newtonsoft.Json;

namespace IP.Mapper.Mapper
{
    public class Mapper<Domain, Request, Response> : IMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
        private readonly IMapper mapper;

        public Mapper(MapperConfiguration configuration)
        {
            mapper = configuration.CreateMapper();
        }

        public Mapper()
        {
            mapper = CreateMapper();
        }

        public virtual Task<string> DomainToJson(Domain item)
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }

        public virtual Task<string> DomainToJson(List<Domain> item)
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }

        public virtual Task<Request> DomainToRequest(Domain item)
        {
            return Task.FromResult(mapper.Map<Request>(item));
        }

        public virtual Task<List<Request>> DomainToRequest(List<Domain> item)
        {
            return Task.FromResult(mapper.Map<List<Request>>(item));
        }

        public virtual Task<Response> DomainToResponse(Domain item)
        {
            return Task.FromResult(mapper.Map<Response>(item));
        }

        public virtual Task<List<Response>> DomainToResponse(List<Domain> item)
        {
            return Task.FromResult(mapper.Map<List<Response>>(item));
        }

        public virtual Task<Domain> JsonToDomain(string item)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<Domain>(item));
        }

        public virtual Task<List<Domain>> JsonToDomainList(string item)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<List<Domain>>(item));
        }

        public virtual Task<Request> JsonToRequest(string item)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<Request>(item));
        }

        public virtual Task<List<Request>> JsonToRequestList(string item)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<List<Request>>(item));
        }

        public virtual Task<Response> JsonToResponse(string item)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<Response>(item));
        }

        public virtual Task<List<Response>> JsonToResponseList(string item)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<List<Response>>(item));
        }

        public virtual Task<Domain> RequestToDomain(Request item)
        {
            return Task.FromResult(mapper.Map<Domain>(item));
        }

        public virtual Task<List<Domain>> RequestToDomain(List<Request> item)
        {
            return Task.FromResult(mapper.Map<List<Domain>>(item));
        }

        public virtual Task<string> RequestToJson(Request item)
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }

        public virtual Task<string> RequestToJson(List<Request> item)
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }

        public virtual Task<Domain> ResponseToDomain(Response item)
        {
            return Task.FromResult(mapper.Map<Domain>(item));
        }

        public virtual Task<List<Domain>> ResponseToDomain(List<Response> item)
        {
            return Task.FromResult(mapper.Map<List<Domain>>(item));
        }

        public virtual Task<string> ResponseToJson(Response item)
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }

        public virtual Task<string> ResponseToJson(List<Response> item)
        {
            return Task.FromResult(JsonConvert.SerializeObject(item));
        }

        private IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain, Request>();
                cfg.CreateMap<Request, Domain>();

                cfg.CreateMap<Domain, Response>();
                cfg.CreateMap<Response, Domain>();
            });

            return configuration.CreateMapper();
        }
    }
}
