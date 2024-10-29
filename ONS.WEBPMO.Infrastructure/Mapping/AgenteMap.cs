using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;


namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class AgenteMap : IEntityTypeConfiguration<Agente>
    {
        public void Configure(EntityTypeBuilder<Agente> builder)
        {

            builder.HasKey(e => e.Id)
                    .HasName("pk_tb_agenteinstituicao")
                    .IsClustered(false);

            builder.ToTable("tb_agenteinstituicao");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id_agenteinstituicao");
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("dsc_apelidorazaosocial");
            builder.Property(e => e.NomeLongo)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}