using IP.BaseDomains;
using System.ComponentModel.DataAnnotations;

namespace IP.Domain
{
    public class UsuarioAcesso : BaseDomain
    {
        [Required]
        public string status { get; set; }
        public string usuarioId { get; set; }
        public string clienteId { get; set; }
        public Guid identificador { get; set; }
    }
}
