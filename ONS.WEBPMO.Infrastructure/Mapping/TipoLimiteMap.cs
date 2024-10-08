using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoLimiteMap : IEntityTypeConfiguration<TipoLimite>
    {
        public void Configure(EntityTypeBuilder<TipoLimite> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Mapeamento da tabela e das colunas
            builder.ToTable("tb_tplimite");

            builder.Property(t => t.Id)
                   .HasColumnName("id_tplimite")
                   .ValueGeneratedNever(); // Equivalente ao antigo DatabaseGeneratedOption.None

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tplimite")
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
