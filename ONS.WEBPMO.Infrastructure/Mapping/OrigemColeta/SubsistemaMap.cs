using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    public class SubsistemaMap : IEntityTypeConfiguration<Subsistema>
    {

        public void Configure(EntityTypeBuilder<Subsistema> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_aux_subsistema");

            // Chave primária
            builder.HasKey(t => t.Id);

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_origemcoleta")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.Codigo)
                   .HasColumnName("cod_subsistema");
        }

    }
}