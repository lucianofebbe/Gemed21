using IP.Application.Comands.Responses.Usuario;
using IP.BaseDomains;
using MediatR;

namespace IP.Application.Comands.Requests.Usuario
{
    public class UsuarioVincularClinicaRequest : BaseRequest, IRequest<UsuarioVincularClinicaResponse>
    {
        public Guid Identificador { get; set; }
        public int ClienteId { get; set; }
        //public Usuario Usuario { get; set; }
    }
}
