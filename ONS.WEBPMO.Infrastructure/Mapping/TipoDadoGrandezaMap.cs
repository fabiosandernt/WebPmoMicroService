using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Models.Mapping
{
    public class TipoDadoGrandezaMap : IEntityTypeConfiguration<TipoDadoGrandeza>
    {
        public void Configure(EntityTypeBuilder<TipoDadoGrandeza> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_tpdadograndeza");

            // Chave prim�ria
            builder.HasKey(t => t.Id);

            // Configura��o das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_tpdadograndeza")
                   .ValueGeneratedNever(); // Equivalente a HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

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
