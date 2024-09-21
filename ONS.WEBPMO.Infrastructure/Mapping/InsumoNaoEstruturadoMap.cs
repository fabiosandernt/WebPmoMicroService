namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class InsumoNaoEstruturadoMap : EntityTypeConfiguration<InsumoNaoEstruturado>
    {
        public InsumoNaoEstruturadoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_insumonaoestruturado");
            Property(t => t.Id).HasColumnName("id_insumopmo");
            Property(t => t.IsUtilizadoDECOMP).HasColumnName("flg_utilizadodecomp");
            Property(t => t.IsUtilizadoConvergencia).HasColumnName("flg_utilizadoconvergencia");
            Property(t => t.IsUtilizadoPublicacao).HasColumnName("flg_utilizadopublicacao");
            Property(t => t.IsUtilizadoProcessamento).HasColumnName("flg_utilizadoprocessamento");

        }
    }
}