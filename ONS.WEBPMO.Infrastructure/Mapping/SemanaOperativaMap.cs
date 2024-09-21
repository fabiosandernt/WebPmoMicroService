namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;

    using Entities;
    using ONS.WEBPMO.Domain.Entities.PMO;

    internal class SemanaOperativaMap : EntityTypeConfiguration<SemanaOperativa>
    {
        public SemanaOperativaMap()
        {
            ToTable("tb_semanaoperativa");
            HasKey(t => t.Id);
            
            Property(t => t.Id).HasColumnName("id_semanaoperativa");
            Property(t => t.Nome).HasColumnName("nom_semanaoperativa").HasMaxLength(150);
            Property(t => t.DataInicioSemana).HasColumnName("dat_iniciosemana");
            Property(t => t.DataFimSemana).HasColumnName("dat_fimsemana");
            Property(t => t.DataReuniao).HasColumnName("dat_reuniao");
            Property(t => t.DataInicioManutencao).HasColumnName("dat_iniciomanutencao");
            Property(t => t.DataFimManutencao).HasColumnName("dat_fimmanutencao");
            Property(t => t.Revisao).HasColumnName("num_revisao").IsRequired();
            Property(t => t.Versao)
                .HasColumnName("ver_controleconcorrencia")
                .IsConcurrencyToken()
                .IsRowVersion();

            Property(t => t.DataHoraAtualizacao).HasColumnName("din_ultimaalteracao");
            Property(t => t.PMOId).HasColumnName("id_pmo");

            HasMany(e => e.Gabaritos).WithOptional(e => e.SemanaOperativa)
                .HasForeignKey(t => t.SemanaOperativaId).WillCascadeOnDelete(false);

            HasMany(e => e.ColetasInsumos).WithRequired(e => e.SemanaOperativa)
                .HasForeignKey(t => t.SemanaOperativaId).WillCascadeOnDelete(false);

            Property(e => e.SituacaoId).HasColumnName("id_tpsituacaosemanaoper");
            HasOptional(e => e.Situacao).WithMany().HasForeignKey(e => e.SituacaoId).WillCascadeOnDelete(false);

            HasMany(t => t.Arquivos).WithRequired(e => e.SemanaOperativa)
                .HasForeignKey(t => t.SemanaOperativaId).WillCascadeOnDelete(true);

            HasOptional(t => t.DadoConvergencia)
                .WithRequired(t => t.SemanaOperativa)
                .Map(m => m.MapKey("id_semanaoperativa"))
                .WillCascadeOnDelete(true);
        }
    }
}