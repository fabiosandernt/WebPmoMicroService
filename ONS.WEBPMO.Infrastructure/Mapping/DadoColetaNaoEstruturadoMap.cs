namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class DadoColetaNaoEstruturadoMap : EntityTypeConfiguration<DadoColetaNaoEstruturado>
    {
        public DadoColetaNaoEstruturadoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_dadocoletanaoestruturado");
            Property(t => t.Id).HasColumnName("id_dadocoleta");
            Property(t => t.Observacao).HasColumnName("obs_dadocoletanaoestruturado").HasMaxLength(1000);

            HasMany(t => t.Arquivos)
                .WithMany()
                .Map(m => {
                    m.ToTable("tb_arqdadocoletanaoestruturado");
                    m.MapLeftKey("id_dadocoleta");
                    m.MapRightKey("id_arquivo");
                });
        }
    }
}