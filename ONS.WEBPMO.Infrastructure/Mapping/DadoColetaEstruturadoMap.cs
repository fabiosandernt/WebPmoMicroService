using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaEstruturadoMap : IEntityTypeConfiguration<DadoColetaEstruturado>
    {
        public void Configure(EntityTypeBuilder<DadoColetaEstruturado> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_dadocoletaestruturado");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_dadocoleta");

            builder.Property(t => t.Valor)
                   .HasColumnName("val_dado")
                   .HasMaxLength(4000);

            builder.Property(t => t.Estagio)
                   .HasColumnName("dsc_estagio")
                   .HasMaxLength(50);

            builder.Property(t => t.TipoLimiteId)
                   .HasColumnName("id_tplimite");

            builder.Property(t => t.TipoPatamarId)
                   .HasColumnName("id_tppatamar");

            // Relacionamentos
            builder.HasOne(t => t.TipoLimite)
                   .WithMany()
                   .HasForeignKey(t => t.TipoLimiteId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional

            builder.HasOne(t => t.TipoPatamar)
                   .WithMany()
                   .HasForeignKey(t => t.TipoPatamarId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional
        }
    }
}