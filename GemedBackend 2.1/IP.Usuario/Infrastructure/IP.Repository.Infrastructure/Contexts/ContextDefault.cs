using IP.DomainsConfiguration.Infrastructure;
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
        }

        public DbSet<Domains.Domain.Usuario> Usuario { get; set; }
        public DbSet<Domains.Domain.Menu> Menu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
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
