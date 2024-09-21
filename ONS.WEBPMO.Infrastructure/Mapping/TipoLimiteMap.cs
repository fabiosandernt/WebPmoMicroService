namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoLimiteMap : EntityTypeConfiguration<TipoLimite>
    {
        public TipoLimiteMap()
        {
            ToTable("tb_tplimite");

            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id).HasColumnName("id_tplimite")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Descricao).HasColumnName("dsc_tplimite")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
