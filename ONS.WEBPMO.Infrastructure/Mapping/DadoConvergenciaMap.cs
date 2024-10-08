using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoConvergenciaMap : IEntityTypeConfiguration<DadoConvergencia>
    {
        public void Configure(EntityTypeBuilder<DadoConvergencia> builder)
        {
            builder.ToTable("tb_dadosconvergencia");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                   .HasColumnName("id_dadosconvergencia")
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.LoginConvergenciaRepresentanteCCEE)
                   .HasColumnName("lgn_representanteccee")
                   .HasMaxLength(60);

            builder.Property(t => t.ObservacaoConvergenciaCCEE)
                   .HasColumnName("obs_ccee")
                   .HasMaxLength(1000);

            // Relacionamento um-para-um com SemanaOperativa
            builder.HasOne(t => t.SemanaOperativa)
                   .WithOne(t => t.DadoConvergencia)
                   .HasForeignKey<DadoConvergencia>(t => t.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
