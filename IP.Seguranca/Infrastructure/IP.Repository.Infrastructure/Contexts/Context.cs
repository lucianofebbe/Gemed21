using Microsoft.EntityFrameworkCore;

namespace IP.Repository.Infrastructure.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }

        public Context() { }

        public DbSet<Domain.SegEmpresa> SegEmpresas { get; set; }
        public DbSet<Domain.SegUsuario> SegUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextOptions).Assembly);
        }
    }
}
