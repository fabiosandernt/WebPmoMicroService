using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class SituacaoSemanaOperativaMap : IEntityTypeConfiguration<SituacaoSemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<SituacaoSemanaOperativa> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Mapeamento da tabela e das colunas
            builder.ToTable("tb_tpsituacaosemanaoper");

            builder.Property(t => t.Id)
                   .HasColumnName("id_tpsituacaosemanaoper")
                   .ValueGeneratedNever(); // Equivalente ao antigo DatabaseGeneratedOption.None

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_situacaosemanaoper")
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
