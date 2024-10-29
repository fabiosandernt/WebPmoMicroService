using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ColetaInsumoMap : IEntityTypeConfiguration<ColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<ColetaInsumo> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_tb_coletainsumo");

            builder.ToTable("tb_coletainsumo");

            builder.HasIndex(e => e.AgenteId, "in_fk_agenteinstituicao_coletainsumo").HasFillFactor(90);

            builder.HasIndex(e => e.InsumoId, "in_fk_insumopmo_coletainsumo").HasFillFactor(90);

            builder.HasIndex(e => e.SemanaOperativaId, "in_fk_semanaoperativa_coletainsumo").HasFillFactor(90);

            builder.HasIndex(e => e.SituacaoId, "in_fk_tpsituacaocoletainsumo_coletainsumo").HasFillFactor(90);

            //builder.Property(e => e.IdColetainsumo).HasColumnName("id_coletainsumo");
            builder.Property(e => e.CodigoPerfilONS)
                .HasMaxLength(30)
                .HasColumnName("cod_perfilons");
            builder.Property(e => e.DataHoraAtualizacao)
                .HasColumnType("datetime")
                .HasColumnName("din_ultimaalteracao");
            builder.Property(e => e.MotivoAlteracaoONS)
                .HasMaxLength(1000)
                .HasColumnName("dsc_motivoalteracaoons");
            builder.Property(e => e.MotivoRejeicaoONS)
                .HasMaxLength(1000)
                .HasColumnName("dsc_motivorejeicaoons");
            builder.Property(e => e.AgenteId).HasColumnName("id_agenteinstituicao");
            builder.Property(e => e.InsumoId).HasColumnName("id_insumopmo");
            builder.Property(e => e.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            builder.Property(e => e.SituacaoId).HasColumnName("id_tpsituacaocoletainsumo");
            builder.Property(e => e.LoginAgenteAlteracao)
                .HasMaxLength(150)
                .HasColumnName("lgn_agenteultimaalteracao");
            builder.Property(e => e.NomesGrandezasNaoEstagioAlteradas)
                .HasMaxLength(1000)
                .HasColumnName("nom_grandezasnaoestagioalteradas");
            builder.Property(e => e.Versao)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("ver_controleconcorrencia");

            builder.HasOne(d => d.Agente).WithMany(p => p.ColetasInsumos)
                .HasForeignKey(d => d.AgenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_agenteinstituicao_coletainsumo");

            builder.HasOne(d => d.Insumo).WithMany(p => p.ColetasInsumo)
                .HasForeignKey(d => d.InsumoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_insumopmo_coletainsumo");

            builder.HasOne(d => d.SemanaOperativa).WithMany(p => p.ColetasInsumos)
                .HasForeignKey(d => d.SemanaOperativaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_semanaoperativa_coletainsumo");

            builder.HasOne(d => d.Situacao).WithMany(p => p.ColetaInsumos)
                .HasForeignKey(d => d.SituacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tpsituacaocoletainsumo_coletainsumo");
        }
    }
}