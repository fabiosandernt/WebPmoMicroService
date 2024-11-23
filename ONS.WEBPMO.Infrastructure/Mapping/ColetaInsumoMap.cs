using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ColetaInsumoMap : IEntityTypeConfiguration<ColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<ColetaInsumo> builder)
        {
            // Configuração das propriedades
             builder.Property(t => t.Id)
                   .HasColumnName("id_coletainsumo");

            builder.Property(t => t.MotivoAlteracaoONS)
                   .HasColumnName("dsc_motivoalteracaoons")
                   .HasMaxLength(1000);

            builder.Property(t => t.MotivoRejeicaoONS)
                   .HasColumnName("dsc_motivorejeicaoons")
                   .HasMaxLength(1000);

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsConcurrencyToken()
                   .IsRowVersion();

            builder.Property(t => t.CodigoPerfilONS)
                   .HasColumnName("cod_perfilons")
                   .HasMaxLength(30);

            builder.Property(t => t.DataHoraAtualizacao)
                   .HasColumnName("din_ultimaalteracao");

            builder.Property(t => t.LoginAgenteAlteracao)
                   .HasColumnName("lgn_agenteultimaalteracao");

            // Ignorar propriedade
            builder.Ignore(t => t.NomeAgentePerfil);

            // Relacionamentos
            builder.Property(e => e.AgenteId)
                   .HasColumnName("id_agenteinstituicao");

            builder.HasOne(t => t.Agente)
                   .WithMany(t => t.ColetasInsumos)
                   .HasForeignKey(t => t.AgenteId);

            builder.Property(e => e.InsumoId)
                   .HasColumnName("id_insumopmo");

            builder.HasOne(t => t.Insumo)
                   .WithMany(t => t.ColetasInsumo)
                   .HasForeignKey(t => t.InsumoId);

            builder.Property(e => e.SituacaoId)
                   .HasColumnName("id_tpsituacaocoletainsumo");

            builder.HasOne(e => e.Situacao)
                   .WithMany()
                   .HasForeignKey(e => e.SituacaoId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao WillCascadeOnDelete(false)

            builder.Property(t => t.SemanaOperativaId)
                   .HasColumnName("id_semanaoperativa");

            // Relacionamento comentado no original
            // builder.HasOne(t => t.SemanaOperativa)
            //        .WithMany(t => t.ColetasInsumos)
            //        .HasForeignKey(t => t.SemanaOperativaId)
            //        .OnDelete(DeleteBehavior.Restrict); // Equivalente ao WillCascadeOnDelete(false)

            builder.Property(t => t.NomesGrandezasNaoEstagioAlteradas)
                   .HasColumnName("nom_grandezasnaoestagioalteradas");
        }
    }
}