using IP.BaseDomains;

namespace IP.Domains.Domain
{
    public class Usuario : BaseDomain
    {
        public int IdUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Tratamento { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string EMail { get; set; }
        public string Telefone { get; set; }
        public string CaminhoFoto { get; set; }
        public int TentativasLogin { get; set; }
        public DateTime? DataHoraBloqueio { get; set; }
        public string Cpf { get; set; }
    }
}
