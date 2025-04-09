using IP.Application.Comands.Responses;
using MediatR;

namespace IP.Application.Comands.Requests
{
    public class Request : IRequest<Response>
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string TipoRequisicao { get; set; }
        public string TipoEmpresa { get; set; }
        public string Empresa { get; set; }
        public string EndPoint { get; set; }
        public Object Objeto { get; set; }
    }
}
