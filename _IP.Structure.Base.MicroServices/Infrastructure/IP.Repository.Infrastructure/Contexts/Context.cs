using Microsoft.EntityFrameworkCore;

namespace IP.Repository.Infrastructure.Contexts
{
    public class Context : DbContext
    {
        private readonly string connectionString;
        public Context(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<Domain.Usuario> Usuarios { get; set; }
        public DbSet<Domain.UsuarioAcesso> UsuarioAcessos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextOptions).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
