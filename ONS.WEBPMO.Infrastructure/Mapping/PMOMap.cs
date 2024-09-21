namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using ONS.WEBPMO.Domain.Entities.PMO;

    internal class PMOMap : EntityTypeConfiguration<PMO>
    {
        public PMOMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_pmo");
            Property(t => t.Id).HasColumnName("id_pmo");
            Property(t => t.AnoReferencia).HasColumnName("ano_referencia");
            Property(t => t.MesReferencia).HasColumnName("mes_referencia");
            Property(t => t.QuantidadeMesesAdiante).HasColumnName("qtd_mesesadiante");
            Property(t => t.Versao)
                .HasColumnName("ver_controleconcorrencia")
                .IsRowVersion();

            HasMany(e => e.SemanasOperativas).WithRequired(e => e.PMO)
                .HasForeignKey(e => e.PMOId);
        }
    }
}