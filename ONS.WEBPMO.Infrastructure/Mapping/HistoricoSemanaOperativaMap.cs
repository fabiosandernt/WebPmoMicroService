using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class HistoricoSemanaOperativaMap : IEntityTypeConfiguration<HistoricoSemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<HistoricoSemanaOperativa> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("tb_histmodifsemanaoper");

            builder.Property(t => t.Id).HasColumnName("id_histmodifsemanaoper");
            builder.Property(t => t.DataHoraAlteracao).HasColumnName("din_histmodifsemanaoper");

            builder.Property(t => t.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            builder.HasOne(t => t.SemanaOperativa)
                   .WithMany()
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .IsRequired();

            builder.Property(t => t.SituacaoId).HasColumnName("id_tpsituacaosemanaoper");
            builder.HasOne(t => t.Situacao)
                   .WithMany()
                   .HasForeignKey(t => t.SituacaoId)
                   .IsRequired();
        }
    }
}
