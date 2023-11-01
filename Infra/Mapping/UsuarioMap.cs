using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infra.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
			builder.ToTable("usuario", "usuario");

			builder.HasKey(prop => prop.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd()
				.HasColumnName("id_usuario");
			builder.HasIndex(prop => new { prop.Id });

			builder.Property(prop => prop.Nome)
				.IsRequired()
				.HasColumnName("desc_usuario")
				.HasColumnType("varchar(100)");

			builder.Property(prop => prop.Login)
				.IsRequired()
				.HasColumnName("desc_login")
				.HasColumnType("varchar(30)");

			builder.Property(prop => prop.Senha)
				.IsRequired()
				.HasColumnName("desc_senha")
				.HasColumnType("varchar(30)");

		}
    }
}
