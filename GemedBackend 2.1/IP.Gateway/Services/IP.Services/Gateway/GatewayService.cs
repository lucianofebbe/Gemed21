using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Domain;
using IP.Logs.LogsSettings;
using IP.Mapper.Infrastructure;
using IP.MongoDb.MongoDbConfig;
using Newtonsoft.Json.Linq;

namespace IP.Services.Gateway
{
    public class GatewayService : IGatewayService
    {
        private readonly Request request;
        private readonly CancellationToken cancellationToken;
        private readonly IServicesFactory iService;
        private readonly IMapperGateway<AuthenticationResponse, Request, Response> iMapperAuthentication;
        private readonly IMapperGateway<AuthorizationResponse, Request, Response> iMapperAuthorization;

        public GatewayService(
            Request request,
            CancellationToken cancellationToken,
            IServicesFactory iService,
            IMapperGateway<AuthenticationResponse, Request, Response> iMapperAuthentication,
            IMapperGateway<AuthorizationResponse, Request, Response> iMapperAuthorization)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iService = iService;
            this.iMapperAuthentication = iMapperAuthentication;
            this.iMapperAuthorization = iMapperAuthorization;
        }

        public async Task<Response> SendRequestAsync()
        {
            var respose = new Response();
            try
            {
                if (request.TipoRequisicao == "Autenticacao")
                {
                    var authenticationRequest = iService
                        .CreateRequestService(request, cancellationToken).Result
                        .RequestAsync().Result;

                    if (authenticationRequest.Message == "OK")
                    {
                        var authentication = iMapperAuthentication.JsonToDomain(authenticationRequest.Objeto.ToString()).Result;
                        if (String.IsNullOrEmpty(authentication.Message))
                        {
                            var objAutorization = new Request()
                            {
                                Objeto = authenticationRequest.Objeto,
                                TipoRequisicao = "Autorizacao",
                                EndPoint = "AutenticacaoHandler"
                            };

                            var autorizationRequest = iService
                                .CreateRequestService(objAutorization, cancellationToken).Result
                                .RequestAsync().Result;

                            if (autorizationRequest.Message == "OK")
                            {
                                var autorization = iMapperAuthorization.JsonToDomain(autorizationRequest.Objeto.ToString()).Result;
                                if (String.IsNullOrEmpty(autorization.Message))
                                {
                                    authentication.Authorization = autorization;
                                    respose.Objeto = authentication;
                                }
                                else
                                {
                                    respose.Message = autorizationRequest.Message;
                                }
                            }
                            else
                            {
                                respose.Message = autorizationRequest.Message;
                            }
                        }
                        else
                        {
                            respose.Message = authentication.Message;
                        }
                    }
                    else
                    {
                        respose.Message = authenticationRequest.Message;
                    }
                }
                else
                {
                    var objParse = request.Objeto != null ? JObject.Parse(request.Objeto.ToString()) : JObject.Parse("{}");
                    objParse.Add("token", request.Token);
                    objParse.Add("refreshToken", request.RefreshToken);

                    var objAutorization = new Request()
                    {
                        Objeto = objParse,
                        TipoRequisicao = "Autorizacao",
                        EndPoint = "AutorizacaoHandler"
                    };

                    var autorizationRequest = iService
                        .CreateRequestService(objAutorization, cancellationToken).Result
                        .RequestAsync().Result;

                    var autorization = iMapperAuthorization.JsonToDomain(autorizationRequest.Objeto.ToString()).Result;
                    if (autorizationRequest.Message == "OK")
                    {
                        if (String.IsNullOrEmpty(autorization.Message))
                        {
                            request.Id = autorization.Id;
                            var responseRequest = iService
                                .CreateRequestService(request, cancellationToken).Result
                                .RequestAsync().Result;

                            if (autorizationRequest.Message == "OK")
                            {
                                respose.Objeto = responseRequest;
                            }
                            else
                            {
                                respose.Message = autorization.Message;
                            }
                        }
                        else
                        {
                            respose.Message = autorization.Message;
                        }
                    }
                    else
                    {
                        respose.Message = autorization.Message;
                    }
                }

                return respose;
            }
            catch (Exception ex)
            {
                var service = iService.CreateLogs().Result;
                _ = service.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                respose.Message = ex.Message;
                return respose;
            }
        }
    }
}
