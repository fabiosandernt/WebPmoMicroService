using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ParametroMap : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_parametropmo");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_parametropmo");

            builder.Property(t => t.Nome)
                   .HasColumnName("nom_parametropmo")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(t => t.Valor)
                   .HasColumnName("val_parametropmo")
                   .HasMaxLength(1000);

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_parametropmo")
                   .HasMaxLength(255);
        }
    }
}
