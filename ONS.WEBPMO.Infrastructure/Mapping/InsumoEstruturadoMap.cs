namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class InsumoEstruturadoMap : EntityTypeConfiguration<InsumoEstruturado>
    {
        public InsumoEstruturadoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_insumoestruturado");
            Property(t => t.Id).HasColumnName("id_insumopmo");
            Property(t => t.QuantidadeMesesAdiante).HasColumnName("qtd_mesesadiante");
            Property(t => t.TipoBloco).HasColumnName("tip_blocomontador");
            Property(t => t.OrdemBlocoMontador).HasColumnName("num_ordemblocomontador");

            // Relationships
            Property(t => t.CategoriaInsumoId).HasColumnName("id_tpcategoriainsumo");
            HasRequired(t => t.CategoriaInsumo).WithMany().HasForeignKey(t => t.CategoriaInsumoId);

            Property(t => t.TipoColetaId).HasColumnName("id_tpcoleta");
            HasOptional(t => t.TipoColeta).WithMany().HasForeignKey(t => t.TipoColetaId);
        }
    }
}