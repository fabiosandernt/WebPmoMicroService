using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaManutencaoMap : IEntityTypeConfiguration<DadoColetaManutencao>
    {
        public void Configure(EntityTypeBuilder<DadoColetaManutencao> builder)
        {

            // Nome da tabela
            builder.ToTable("tb_dadocoletamanutencao");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_dadocoleta");

            builder.Property(t => t.DataInicio)
                   .HasColumnName("dat_inicio");

            builder.Property(t => t.DataFim)
                   .HasColumnName("dat_fim");

            builder.Property(t => t.TempoRetorno)
                   .HasColumnName("prd_temporetorno")
                   .HasMaxLength(5);

            builder.Property(t => t.Justificativa)
                   .HasColumnName("dsc_justificativa")
                   .IsRequired()
                   .HasMaxLength(4000);

            builder.Property(t => t.Numero)
                   .HasColumnName("num_manutencao")
                   .HasMaxLength(20);

            builder.Property(t => t.UnidadeGeradoraId)
                   .HasColumnName("id_origemcoletauge");

            builder.Property(t => t.Situacao)
                   .HasColumnName("nom_situacao");

            builder.Property(t => t.ClassificacaoPorTipoEquipamento)
                   .HasColumnName("sgl_classificacaotpeqpintervencao");

            builder.Property(t => t.Periodicidade)
                   .HasColumnName("sgl_periodicidadeintervencao");

            // Relacionamento opcional com UnidadeGeradora
            builder.HasOne(t => t.UnidadeGeradora)
                   .WithMany()
                   .HasForeignKey(t => t.UnidadeGeradoraId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional
        }
    }
}