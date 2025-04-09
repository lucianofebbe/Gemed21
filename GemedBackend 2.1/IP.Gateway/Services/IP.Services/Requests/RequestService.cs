using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Logs.LogsSettings;
using IP.MongoDb.MongoDbConfig;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace IP.Services.Requests
{
    public class RequestService : IRequestService
    {
        private readonly Request request;
        private readonly CancellationToken cancellationToken;
        private readonly IServicesFactory iService;

        public RequestService(
            Request request,
            CancellationToken cancellationToken,
            IServicesFactory iService)
        {
            this.request = request;
            this.cancellationToken = cancellationToken;
            this.iService = iService;
        }

        public async Task<Response> RequestAsync()
        {
            var result = new Response();
            try
            {
                var mongoDb = iService
                    .CreateMongoDbService(request.TipoRequisicao, request.TipoEmpresa, request.Empresa, cancellationToken).Result;

                var typeAndCompany = mongoDb.GetTypeAndCompanyAsync().Result;
                if (typeAndCompany.Item1 != null && typeAndCompany.Item2 != null)
                {
                    var objParse = request.Objeto != null ? JObject.Parse(request.Objeto.ToString()) : JObject.Parse("{}");
                    objParse.Add("connectionString", typeAndCompany.Item2.ConnectionString);
                    objParse.Add("providerName", typeAndCompany.Item2.ProviderName);
                    var jsonNode = JsonNode.Parse(objParse.ToString());
                    using (var httpClient = new HttpClient())
                    {
                        var response = new HttpResponseMessage();
#if DEBUG
                        response = await httpClient.PostAsJsonAsync
                            (typeAndCompany.Item1.HostNameDesenv + "/" + request.EndPoint, jsonNode, cancellationToken);
#else
                        response = await httpClient.PostAsJsonAsync
                            (typeAndCompany.Item1.HostName + "/" + request.EndPoint, jsonNode, cancellationToken);
#endif
                        result.Message = response.StatusCode.ToString();
                        if (response.IsSuccessStatusCode)
                            result.Objeto = response.Content.ReadAsStringAsync().Result;
                        else
                            result.Message = "Ocorreu um erro na solicitação. " + response.ReasonPhrase;
                    }
                }
                else
                    result.Message = "Nao foi possivel encontrar o tipo de requisição, empresa e ou objeto para requisição";

                return result;
            }
            catch (Exception ex)
            {
                var service = iService.CreateLogs().Result;
                _ = service.Create(new LogsSettings(false, TypeLogs.System), ex, new MongoConfig(), ex.Message)
                    .GenerateLog();
                result.Message = ex.Message;
                if (ex.InnerException != null)
                    result.Message += Environment.NewLine + ex.InnerException.ToString();
                return result;
            }
        }
    }
}
