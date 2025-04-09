using AutoMapper;
using Newtonsoft.Json;

namespace IP.Mapper.Infrastructure
{
    public class MapperGateway<Domain, Request, Response> : IMapperGateway<Domain, Request, Response>
    {
        private readonly IMapper mapper;

        public MapperGateway()
        {
            mapper = CreateMapper();
        }
        public MapperGateway(MapperConfiguration configuration)
        {
            mapper = configuration.CreateMapper();
        }

        public virtual Task<string> DomainToJson(Domain item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<string> DomainToJson(List<Domain> item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Request> DomainToRequest(Domain item)
        {
            try
            {
                return Task.FromResult(mapper.Map<Request>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Request>> DomainToRequest(List<Domain> item)
        {
            try
            {
                return Task.FromResult(mapper.Map<List<Request>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Response> DomainToResponse(Domain item)
        {
            try
            {
                return Task.FromResult(mapper.Map<Response>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Response>> DomainToResponse(List<Domain> item)
        {
            try
            {
                return Task.FromResult(mapper.Map<List<Response>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Domain> JsonToDomain(string item)
        {
            try
            {
                return Task.FromResult(JsonConvert.DeserializeObject<Domain>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Domain>> JsonToDomainList(string item)
        {
            try
            {
                return Task.FromResult(JsonConvert.DeserializeObject<List<Domain>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Request> JsonToRequest(string item)
        {
            try
            {
                return Task.FromResult(JsonConvert.DeserializeObject<Request>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Request>> JsonToRequestList(string item)
        {
            try
            {
                return Task.FromResult(JsonConvert.DeserializeObject<List<Request>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Response> JsonToResponse(string item)
        {
            try
            {
                return Task.FromResult(JsonConvert.DeserializeObject<Response>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Response>> JsonToResponseList(string item)
        {
            try
            {
                return Task.FromResult(JsonConvert.DeserializeObject<List<Response>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Domain> RequestToDomain(Request item)
        {
            try
            {
                return Task.FromResult(mapper.Map<Domain>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Domain>> RequestToDomain(List<Request> item)
        {
            try
            {
                return Task.FromResult(mapper.Map<List<Domain>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<string> RequestToJson(Request item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<string> RequestToJson(List<Request> item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<Domain> ResponseToDomain(Response item)
        {
            try
            {
                return Task.FromResult(mapper.Map<Domain>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<List<Domain>> ResponseToDomain(List<Response> item)
        {
            try
            {
                return Task.FromResult(mapper.Map<List<Domain>>(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<string> ResponseToJson(Response item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual Task<string> ResponseToJson(List<Response> item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IMapper CreateMapper()
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
