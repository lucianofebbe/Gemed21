using IP.BaseDomains;
using System.ComponentModel.DataAnnotations;

namespace IP.Domain
{
    public class Cliente : BaseDomain
    {
        [Required(ErrorMessage = "NomeInterno must have a value")]
        public string NomeInterno { get; set; }

        [Required(ErrorMessage = "BaseDados must have a value")]
        public string BaseDados { get; set; }
        
        [Required(ErrorMessage = "Fantasia must have a value")]
        public string Fantasia { get; set; }
        
        [Required(ErrorMessage = "RazaoSocial must have a value")]
        public string RazaoSocial { get; set; }
        
        public byte[]? Logotipo { get; set; }
    }
}
