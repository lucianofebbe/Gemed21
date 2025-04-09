using IP.Domains.Domain;
using IP.UnitEntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IP.Repository.Infrastructure.Contexts
{
    public class ContextDefault : UnitContext
    {
        private readonly string connectionString;
        private readonly string providerName;
        public ContextDefault(string connectionString = "", string providerName = "")
        {
            this.connectionString = connectionString;
            this.providerName = providerName;

#if DEBUG
            this.connectionString += "TrustServerCertificate=true;";
#endif
        }

        public DbSet<SegEmpresa> Usuarios { get; set; }
        public DbSet<SegUsuario> UsuarioAcessos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextOptions).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (providerName)
            {
                case "System.Data.SqlClient":
                    if (!optionsBuilder.IsConfigured)
                        optionsBuilder.UseSqlServer(connectionString);
                    break;
            }
        }
    }
}
