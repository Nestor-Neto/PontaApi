using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enumerator;
using System.Reflection.Emit;
using System.Security.Claims;

namespace Infra.Mapping
{
    public class TarefasMap : IEntityTypeConfiguration<Tarefas>
    {
        public void Configure(EntityTypeBuilder<Tarefas> builder)
        {
            builder.ToTable("tarefas", "tarefas");

            builder.HasKey(prop => prop.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd()
                .HasColumnName("id_tarefa");
            builder.HasIndex(prop => new { prop.Id });

            builder.Property(prop => prop.Titulo)
                .IsRequired()
                .HasColumnName("titulo")
                .HasColumnType("varchar(80)");

            builder.Property(prop => prop.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar(200)");

            builder.Property(prop => prop.Data)
                .IsRequired()
                .HasColumnName("data");

            builder.Property(prop => prop.Status)
           .IsRequired()
           .HasColumnName("status")
           .HasColumnType("varchar(2)")
           .HasConversion(
               v => v.ReturnDescription(), 
               v => Enumerator.ReturnValue<Status>(v)
           );

            builder.Property(prop => prop.IdUsuario)
                .IsRequired()
                .HasColumnName("id_usuario");

            builder.HasOne(prop => prop.Usuario)
                .WithMany()
                .IsRequired()
                .HasForeignKey(prop => prop.IdUsuario)
                .HasConstraintName("fk_usuario")
                .OnDelete(DeleteBehavior.Restrict);




        }
    }
}
