using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Models.Mapping
{
    public class TipoDadoGrandezaMap : IEntityTypeConfiguration<TipoDadoGrandeza>
    {
        public void Configure(EntityTypeBuilder<TipoDadoGrandeza> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Mapeamento da tabela e das colunas
            builder.ToTable("tb_tpdadograndeza");

            builder.Property(t => t.Id)
                   .HasColumnName("id_tpdadograndeza")
                   .ValueGeneratedNever(); // Equivalente ao antigo DatabaseGeneratedOption.None

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tpdadograndeza")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.UsoPmo)
                   .HasColumnName("flg_pmo")
                   .IsRequired();

            builder.Property(t => t.UsoMontador)
                   .HasColumnName("flg_montador")
                   .IsRequired();
        }
    }
}
