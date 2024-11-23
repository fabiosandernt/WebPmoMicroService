using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class SituacaoColetaInsumoMap : IEntityTypeConfiguration<SituacaoColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<SituacaoColetaInsumo> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_tpsituacaocoletainsumo");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_tpsituacaocoletainsumo")
                   .ValueGeneratedNever(); // Equivalente a HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tpsituacaocoletainsumo")
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
