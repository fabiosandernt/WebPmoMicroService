using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{

    public class OrigemColetaMap : IEntityTypeConfiguration<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta>
    {
        public void Configure(EntityTypeBuilder<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> builder)
        {
            builder.ToTable("tb_origemcoleta");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_origemcoleta").HasMaxLength(50).IsRequired();
            builder.Property(t => t.TipoOrigemColetaDescricao).HasColumnName("tip_origemcoleta").IsRequibuilder.red();

            builder.Ignore(t => t.TipoOrigembuilder.Coleta);

            builder.Property(m => m.Nome).HasColumnName("nom_exibicao").HasMaxLength(300).IsRequired();
        }
    }
}