namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using ONS.WEBPMO.Domain.Entities.PMO;

    public class SituacaoSemanaOperativaMap : EntityTypeConfiguration<SituacaoSemanaOperativa>
    {
        public SituacaoSemanaOperativaMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("tb_tpsituacaosemanaoper");
            Property(t => t.Id).HasColumnName("id_tpsituacaosemanaoper").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Descricao)
                .HasColumnName("dsc_situacaosemanaoper")
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
