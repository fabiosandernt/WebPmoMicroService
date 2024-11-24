using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    public class UnidadeGeradoraMap : IEntityTypeConfiguration<UnidadeGeradora>
    {
        public void Configure(EntityTypeBuilder<UnidadeGeradora> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_aux_unidadegeradora");



            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_origemcoleta")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.PotenciaNominal)
                   .HasColumnName("val_potencianominal");

            builder.Property(t => t.CodigoDPP)
                   .HasColumnName("cod_dpp");

            builder.Property(t => t.NumeroConjunto)
                   .HasColumnName("num_conjunto");

            builder.Property(t => t.NumeroMaquina)
                   .HasColumnName("num_maquina");

            builder.Property(t => t.UsinaId)
                   .HasColumnName("id_origemcoletausinapai");

            // Relacionamento com Usina
            builder.HasOne(t => t.Usina)
                   .WithMany(t => t.UnidadesGeradoras)
                   .HasForeignKey(t => t.UsinaId);
        }

    }
}