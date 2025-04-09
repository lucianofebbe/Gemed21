using IP.Application.Comands.Requests;
using IP.Application.Comands.Responses;
using IP.Domain;
using IP.Mapper.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IP.Services.Proxy
{
    public class ProxyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServicesFactory iServices;
        private readonly CancellationToken cancellationToken;

        public ProxyMiddleware(
            RequestDelegate next,
            IServicesFactory iServices)
        {
            _next = next;
            this.iServices = iServices;
        }

        public async Task<Response> Invoke(HttpContext context)
        {
            var response = new Response();
            if (context.Request.Scheme.Contains("http") || context.Request.Scheme.Contains("https"))
            {
                var urls = context.Request.Path.ToString().Split("/").ToList().Where(o => !string.IsNullOrEmpty(o)).ToList();
                var tipoEmpresa = context.Request.Headers["TipoEmpresa"];
                var empresa = context.Request.Headers["Empresa"];
                var token = context.Request.Headers["Token"];
                var refreshToken = context.Request.Headers["RefreshToken"];
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

                var requestObj = new Request()
                {
                    TipoRequisicao = urls[0],
                    EndPoint = urls[1],
                    TipoEmpresa = tipoEmpresa,
                    Empresa = empresa,
                    Token = token,
                    RefreshToken = refreshToken,
                    Objeto = body
                };

                var service = iServices.CreateGatewayService(requestObj, cancellationToken).Result;
                response = await service.SendRequestAsync();
                return response;
            }
            else
            {
                await _next(context);
            }

            return response;
        }
    }
}
