namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class HistoricoColetaInsumoMap : EntityTypeConfiguration<HistoricoColetaInsumo>
    {
        public HistoricoColetaInsumoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_histmodifcoletainsumo");
            Property(t => t.Id).HasColumnName("id_histmodifcoletainsumo");
            Property(t => t.DataHoraAlteracao).HasColumnName("din_histmodifcoletainsumo");

            Property(t => t.ColetaInsumoId).HasColumnName("id_coletainsumo");
            HasRequired(t => t.ColetaInsumo)
                .WithMany()
                .HasForeignKey(t => t.ColetaInsumoId);

            Property(t => t.SituacaoId).HasColumnName("id_tpsituacaocoletainsumo");
            HasRequired(t => t.Situacao)
                .WithMany()
                .HasForeignKey(t => t.SituacaoId);
        }
    }
}