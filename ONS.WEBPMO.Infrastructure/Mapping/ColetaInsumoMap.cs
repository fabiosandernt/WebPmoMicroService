namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class ColetaInsumoMap : EntityTypeConfiguration<ColetaInsumo>
    {
        public ColetaInsumoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_coletainsumo");
            Property(t => t.Id).HasColumnName("id_coletainsumo");
            Property(t => t.MotivoAlteracaoONS).HasColumnName("dsc_motivoalteracaoons").HasMaxLength(1000);
            Property(t => t.MotivoRejeicaoONS).HasColumnName("dsc_motivorejeicaoons").HasMaxLength(1000);
            Property(t => t.Versao)
                .HasColumnName("ver_controleconcorrencia")
                .IsConcurrencyToken()
                .IsRowVersion();

            Property(t => t.CodigoPerfilONS).HasColumnName("cod_perfilons").HasMaxLength(30);
            Property(t => t.DataHoraAtualizacao).HasColumnName("din_ultimaalteracao");
            Property(t => t.LoginAgenteAlteracao).HasColumnName("lgn_agenteultimaalteracao");
            Ignore(t => t.NomeAgentePerfil);

            // Relationships
            Property(e => e.AgenteId).HasColumnName("id_agenteinstituicao");
            HasRequired(t => t.Agente)
                .WithMany(t => t.ColetasInsumos)
                .HasForeignKey(t => t.AgenteId);

            Property(e => e.InsumoId).HasColumnName("id_insumopmo");
            HasRequired(t => t.Insumo)
                .WithMany(t => t.ColetasInsumo)
                .HasForeignKey(t => t.InsumoId);

            Property(e => e.SituacaoId).HasColumnName("id_tpsituacaocoletainsumo");
            HasOptional(e => e.Situacao)
                .WithMany()
                .HasForeignKey(e => e.SituacaoId).WillCascadeOnDelete(false);

            Property(t => t.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            //HasRequired(t => t.SemanaOperativa).WithMany(t => t.ColetasInsumos)
            //    .HasForeignKey(t => t.SemanaOperativaId).WillCascadeOnDelete(false);

            Property(t => t.NomesGrandezasNaoEstagioAlteradas).HasColumnName("nom_grandezasnaoestagioalteradas");

        }
    }
}