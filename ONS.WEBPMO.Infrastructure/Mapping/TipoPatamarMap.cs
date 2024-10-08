using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoPatamarMap : IEntityTypeConfiguration<TipoPatamar>
    {
        public void Configure(EntityTypeBuilder<TipoPatamar> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Mapeamento da tabela e das colunas
            builder.ToTable("tb_tppatamar");

            builder.Property(t => t.Id)
                   .HasColumnName("id_tppatamar")
                   .ValueGeneratedNever(); // Equivalente ao antigo DatabaseGeneratedOption.None

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tppatamar")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.ValorVermelho)
                   .HasColumnName("val_vermelho");

            builder.Property(t => t.ValorVerde)
                   .HasColumnName("val_verde");

            builder.Property(t => t.ValorAzul)
                   .HasColumnName("val_azul");
        }
    }
}
