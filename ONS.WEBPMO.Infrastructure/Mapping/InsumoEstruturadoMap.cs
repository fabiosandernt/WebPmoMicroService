using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class InsumoEstruturadoMap : IEntityTypeConfiguration<InsumoEstruturado>
    {
        public void Configure(EntityTypeBuilder<InsumoEstruturado> builder)
        {
            //builder.HasKey(t => t.Id);

            builder.ToTable("tb_insumoestruturado");

            builder.Property(t => t.Id).HasColumnName("id_insumopmo");
            builder.Property(t => t.QuantidadeMesesAdiante).HasColumnName("qtd_mesesadiante");
            builder.Property(t => t.TipoBloco).HasColumnName("tip_blocomontador");
            builder.Property(t => t.OrdemBlocoMontador).HasColumnName("num_ordemblocomontador");

            // Relacionamentos
            builder.Property(t => t.CategoriaInsumoId).HasColumnName("id_tpcategoriainsumo");
            builder.HasOne(t => t.CategoriaInsumo)
                   .WithMany()
                   .HasForeignKey(t => t.CategoriaInsumoId)
                   .IsRequired();

            builder.Property(t => t.TipoColetaId).HasColumnName("id_tpcoleta");
            builder.HasOne(t => t.TipoColeta)
                   .WithMany()
                   .HasForeignKey(t => t.TipoColetaId)
                   .IsRequired(false); // Relacionamento opcional
        }
    }
}
