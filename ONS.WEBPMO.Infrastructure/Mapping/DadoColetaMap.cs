using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaMap : IEntityTypeConfiguration<DadoColeta>
    {
        public void Configure(EntityTypeBuilder<DadoColeta> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_dadocoleta");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_dadocoleta");

            builder.Property(t => t.TipoDadoColeta)
                   .HasColumnName("tip_dadocoleta")
                   .IsRequired()
                   .IsUnicode(false)
                   .IsFixedLength()
                   .HasMaxLength(1);

            // Relacionamentos
            builder.Property(t => t.ColetaInsumoId)
                   .HasColumnName("id_coletainsumo");

            builder.HasOne(t => t.ColetaInsumo)
                   .WithMany(t => t.DadosColeta)
                   .HasForeignKey(t => t.ColetaInsumoId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            builder.Property(t => t.GabaritoId)
                   .HasColumnName("id_gabarito");

            builder.HasOne(t => t.Gabarito)
                   .WithMany(t => t.DadosColeta)
                   .HasForeignKey(t => t.GabaritoId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao WillCascadeOnDelete(false)

            builder.Property(t => t.GrandezaId)
                   .HasColumnName("id_grandeza");

            builder.HasOne(t => t.Grandeza)
                   .WithMany(t => t.DadosColeta)
                   .HasForeignKey(t => t.GrandezaId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional
        }
    }
}