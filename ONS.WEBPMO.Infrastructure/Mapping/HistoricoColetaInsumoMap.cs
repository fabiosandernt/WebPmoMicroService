using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class HistoricoColetaInsumoMap : IEntityTypeConfiguration<HistoricoColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<HistoricoColetaInsumo> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_histmodifcoletainsumo");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_histmodifcoletainsumo");

            builder.Property(t => t.DataHoraAlteracao)
                   .HasColumnName("din_histmodifcoletainsumo");

            builder.Property(t => t.ColetaInsumoId)
                   .HasColumnName("id_coletainsumo");

            builder.Property(t => t.SituacaoId)
                   .HasColumnName("id_tpsituacaocoletainsumo");

            // Relacionamento com ColetaInsumo
            builder.HasOne(t => t.ColetaInsumo)
                   .WithMany()
                   .HasForeignKey(t => t.ColetaInsumoId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            // Relacionamento com Situacao
            builder.HasOne(t => t.Situacao)
                   .WithMany()
                   .HasForeignKey(t => t.SituacaoId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired
        }
    }
}
