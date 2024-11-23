using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoConvergenciaMap : IEntityTypeConfiguration<DadoConvergencia>
    {
        public void Configure(EntityTypeBuilder<DadoConvergencia> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_dadosconvergencia");

            // Chave primária
            builder.HasKey(t => t.Id);

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_dadosconvergencia")
                   .ValueGeneratedOnAdd(); // Equivalente a HasDatabaseGeneratedOption.Identity

            builder.Property(t => t.LoginConvergenciaRepresentanteCCEE)
                   .HasColumnName("lgn_representanteccee")
                   .HasMaxLength(60);

            builder.Property(t => t.ObservacaoConvergenciaCCEE)
                   .HasColumnName("obs_ccee")
                   .HasMaxLength(1000);
        }
    }
}
