using AutoMapper;
using IP.BaseDomains;
using IP.Logs.LogsFactory;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using Newtonsoft.Json;

namespace IP.Mapper.Mapper
{
    public class Mapper<Domain, Request, Response> : IMapper<Domain, Request, Response>
        where Domain : BaseDomain
        where Request : BaseRequest
        where Response : BaseResponse
    {
        private readonly IMapper mapper;
        private readonly ILogsFactory logsFactory;


        public Mapper(ILogsFactory iGenerateLogs, MapperConfiguration configuration)
        {
            mapper = configuration.CreateMapper();
            this.logsFactory = iGenerateLogs;
        }

        public virtual Task<string> DomainToJson(Domain item)
        {
            try
            {
                return Task.FromResult(JsonConvert.SerializeObject(item));
            }
            catch (Exception ex)
            {
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
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
                _ = logsFactory.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
