using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class HistoricoSemanaOperativaMap : IEntityTypeConfiguration<HistoricoSemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<HistoricoSemanaOperativa> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_histmodifsemanaoper");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_histmodifsemanaoper");

            builder.Property(t => t.DataHoraAlteracao)
                   .HasColumnName("din_histmodifsemanaoper");

            builder.Property(t => t.SemanaOperativaId)
                   .HasColumnName("id_semanaoperativa");

            builder.Property(t => t.SituacaoId)
                   .HasColumnName("id_tpsituacaosemanaoper");

            // Relacionamento com SemanaOperativa
            builder.HasOne(t => t.SemanaOperativa)
                   .WithMany()
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            // Relacionamento com Situacao
            builder.HasOne(t => t.Situacao)
                   .WithMany()
                   .HasForeignKey(t => t.SituacaoId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired
        }
    }
}
