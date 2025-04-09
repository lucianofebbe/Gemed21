using IP.Domains.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IP.DomainsConfiguration.Infrastructure
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(entity => entity.IdUsuario);
            builder.Property(entity => entity.TipoUsuario).HasMaxLength(25).IsRequired();
            builder.Property(entity => entity.Hash).HasMaxLength(50).IsRequired();
            builder.Property(entity => entity.Salt).HasMaxLength(50);
            builder.Property(entity => entity.Nome).HasMaxLength(100);
            builder.Property(entity => entity.Apelido).HasMaxLength(20);
            builder.Property(entity => entity.Tratamento).HasMaxLength(6);
            builder.Property(entity => entity.DataNascimento);
            builder.Property(entity => entity.EMail).HasMaxLength(50).IsRequired();
            builder.Property(entity => entity.Telefone).HasMaxLength(30);
            builder.Property(entity => entity.CaminhoFoto).HasMaxLength(100);
            builder.Property(entity => entity.TentativasLogin).IsRequired();
            builder.Property(entity => entity.DataHoraBloqueio);
            builder.Property(entity => entity.Cpf).HasMaxLength(11).IsRequired();
            //builder.Property(entity => entity.Status).IsRequired();
        }
    }
}
