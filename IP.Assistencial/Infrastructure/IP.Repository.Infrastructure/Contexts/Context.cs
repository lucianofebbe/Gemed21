using Microsoft.EntityFrameworkCore;

namespace IP.Repository.Infrastructure.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public Context() { }

        public DbSet<Domain.Usuario> Usuarios { get; set; }
        public DbSet<Domain.UsuarioAcesso> UsuarioAcessos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextOptions).Assembly);
        }
    }
}
