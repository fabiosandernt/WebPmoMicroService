namespace ONS.WEBPMO.Domain.Repositories.Impl.Models.Mapping
{
    using ONS.WEBPMO.Domain.Entities.PMO;

    public class TipoDadoGrandezaMap : EntityTypeConfiguration<TipoDadoGrandeza>
    {
        public TipoDadoGrandezaMap()
        {
            ToTable("tb_tpdadograndeza");
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id).HasColumnName("id_tpdadograndeza").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Descricao).HasColumnName("dsc_tpdadograndeza").IsRequired().HasMaxLength(20);
            Property(t => t.UsoPmo).HasColumnName("flg_pmo").IsRequired();
            Property(t => t.UsoMontador).HasColumnName("flg_montador").IsRequired();

        }
    }
}
