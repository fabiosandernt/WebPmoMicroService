namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class DadoColetaManutencaoMap : EntityTypeConfiguration<DadoColetaManutencao>
    {
        public DadoColetaManutencaoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_dadocoletamanutencao");
            Property(t => t.Id).HasColumnName("id_dadocoleta");
            Property(t => t.DataInicio).HasColumnName("dat_inicio");
            Property(t => t.DataFim).HasColumnName("dat_fim");
            Property(t => t.TempoRetorno).HasColumnName("prd_temporetorno").HasMaxLength(5);
            Property(t => t.Justificativa).HasColumnName("dsc_justificativa").IsRequired().HasMaxLength(4000);
            Property(t => t.Numero).HasColumnName("num_manutencao").HasMaxLength(20);
            Property(t => t.UnidadeGeradoraId).HasColumnName("id_origemcoletauge");
            Property(t => t.Situacao).HasColumnName("nom_situacao");
            Property(t => t.ClassificacaoPorTipoEquipamento).HasColumnName("sgl_classificacaotpeqpintervencao");
            Property(t => t.Periodicidade).HasColumnName("sgl_periodicidadeintervencao");
            /* comentado devido a replicacao de codigo da branche sprint18_Web-PMO_Bug-76601
            HasOptional(t => t.UnidadeGeradora)
                .WithMany()
                .HasForeignKey(t => t.UnidadeGeradoraId);
            */

        }
    }
}