using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class HistoricoColetaInsumoMap : IEntityTypeConfiguration<HistoricoColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<HistoricoColetaInsumo> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("tb_histmodifcoletainsumo");

            builder.Property(t => t.Id).HasColumnName("id_histmodifcoletainsumo");
            builder.Property(t => t.DataHoraAlteracao).HasColumnName("din_histmodifcoletainsumo");

            builder.Property(t => t.ColetaInsumoId).HasColumnName("id_coletainsumo");
            builder.HasOne(t => t.ColetaInsumo)
                   .WithMany()
                   .HasForeignKey(t => t.ColetaInsumoId)
                   .IsRequired();

            builder.Property(t => t.SituacaoId).HasColumnName("id_tpsituacaocoletainsumo");
            builder.HasOne(t => t.Situacao)
                   .WithMany()
                   .HasForeignKey(t => t.SituacaoId)
                   .IsRequired();
        }
    }
}
