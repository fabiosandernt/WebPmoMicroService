namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class AgenteMap : EntityTypeConfiguration<Agente>
    {
        public AgenteMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_agenteinstituicao");
            Property(t => t.Id).HasColumnName("id_agenteinstituicao")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Nome).HasColumnName("dsc_apelidorazaosocial").IsRequired().HasMaxLength(50);
            Property(t => t.NomeLongo).HasColumnName("dsc_razaosocial").IsRequired().HasMaxLength(100);

        }
    }
}