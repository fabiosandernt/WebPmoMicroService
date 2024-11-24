using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class InsumoNaoEstruturadoMap : IEntityTypeConfiguration<InsumoNaoEstruturado>
    {
        public void Configure(EntityTypeBuilder<InsumoNaoEstruturado> builder)
        {

            // Nome da tabela
            builder.ToTable("tb_insumonaoestruturado");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_insumopmo");

            builder.Property(t => t.IsUtilizadoDECOMP)
                   .HasColumnName("flg_utilizadodecomp");

            builder.Property(t => t.IsUtilizadoConvergencia)
                   .HasColumnName("flg_utilizadoconvergencia");

            builder.Property(t => t.IsUtilizadoPublicacao)
                   .HasColumnName("flg_utilizadopublicacao");

            builder.Property(t => t.IsUtilizadoProcessamento)
                   .HasColumnName("flg_utilizadoprocessamento");
        }
    }
}
