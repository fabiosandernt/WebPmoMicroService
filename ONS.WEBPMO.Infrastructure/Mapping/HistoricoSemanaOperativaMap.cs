namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class HistoricoSemanaOperativaMap : EntityTypeConfiguration<HistoricoSemanaOperativa>
    {
        public HistoricoSemanaOperativaMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_histmodifsemanaoper");
            Property(t => t.Id).HasColumnName("id_histmodifsemanaoper");
            Property(t => t.DataHoraAlteracao).HasColumnName("din_histmodifsemanaoper");

            Property(t => t.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            HasRequired(t => t.SemanaOperativa)
                .WithMany()
                .HasForeignKey(t => t.SemanaOperativaId);

            Property(t => t.SituacaoId).HasColumnName("id_tpsituacaosemanaoper");
            HasRequired(t => t.Situacao)
                .WithMany()
                .HasForeignKey(t => t.SituacaoId);
        }
    }
}