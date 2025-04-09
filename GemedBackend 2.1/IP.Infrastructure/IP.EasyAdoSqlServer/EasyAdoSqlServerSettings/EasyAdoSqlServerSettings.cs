using System.ComponentModel.DataAnnotations;

namespace IP._EasyAdoSqlServer.EasyAdoSqlServerConfig
{
    public class EasyAdoSqlServerSettings
    {
        [Required(ErrorMessage ="No ConnectionString")]
        public string ConnectionString { get; set; }
        public int TimeOut { get; set; }
    }
}
