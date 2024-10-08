using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaMap : IEntityTypeConfiguration<DadoColeta>
    {
        public void Configure(EntityTypeBuilder<DadoColeta> builder)
        {
            builder.HasKey(t => t.Id);
            builder.ToTable("tb_dadocoleta");
            builder.Property(t => t.Id).HasColumnName("id_dadocoleta");
            builder.Property(t => t.TipoDadoColeta)
                .HasColumnName("tip_dadocoleta")
                .IsRequired()
                .IsUnicode(false)
                .IsFixedLength()
                .HasMaxLength(1);

            // Relationships
            builder.Property(t => t.ColetaInsumoId)
                .HasColumnName("id_coletainsumo");
            builder.HasOne(t => t.ColetaInsumo)
                .WithMany(t => t.DadosColeta)
                .HasForeignKey(t => t.ColetaInsumoId);

            builder.Property(t => t.GabaritoId)
                .HasColumnName("id_gabarito");
            builder.HasOne(t => t.Gabarito)
                .WithMany(t => t.DadosColeta)
                .HasForeignKey(t => t.GabaritoId);
                //.OnDelete(false);

            builder.Property(t => t.GrandezaId)
                .HasColumnName("id_grandeza");
            builder.HasOne(t => t.Grandeza)
                .WithMany(t => t.DadosColeta)
                .HasForeignKey(t => t.GrandezaId);
        }
    }
}