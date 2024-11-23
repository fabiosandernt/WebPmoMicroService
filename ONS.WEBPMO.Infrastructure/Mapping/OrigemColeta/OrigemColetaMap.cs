using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{

    public class OrigemColetaMap : IEntityTypeConfiguration<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta>
    {

        public void Configure(EntityTypeBuilder<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_origemcoleta");

            // Chave primária
            builder.HasKey(t => t.Id);

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_origemcoleta")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.TipoOrigemColetaDescricao)
                   .HasColumnName("tip_origemcoleta")
                   .IsRequired();

            // Ignorar a propriedade TipoOrigemColeta
            builder.Ignore(t => t.TipoOrigemColeta);

            builder.Property(m => m.Nome)
                   .HasColumnName("nom_exibicao")
                   .HasMaxLength(300)
                   .IsRequired();
        }
    }
}