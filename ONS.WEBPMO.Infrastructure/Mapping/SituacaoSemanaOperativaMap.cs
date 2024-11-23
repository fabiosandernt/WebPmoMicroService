using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class SituacaoSemanaOperativaMap : IEntityTypeConfiguration<SituacaoSemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<SituacaoSemanaOperativa> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_tpsituacaosemanaoper");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_tpsituacaosemanaoper")
                   .ValueGeneratedNever(); // Equivalente a HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_situacaosemanaoper")
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
