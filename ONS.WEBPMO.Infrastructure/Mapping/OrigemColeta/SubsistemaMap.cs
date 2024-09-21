namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    internal class SubsistemaMap : EntityTypeConfiguration<Subsistema>
    {
        public SubsistemaMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_aux_subsistema");
            Property(t => t.Id).HasColumnName("id_origemcoleta").HasMaxLength(50).IsRequired();
            Property(t => t.Codigo).HasColumnName("cod_subsistema");
        }
    }
}