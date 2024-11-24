using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class InsumoEstruturadoMap : IEntityTypeConfiguration<InsumoEstruturado>
    {
        public void Configure(EntityTypeBuilder<InsumoEstruturado> builder)
        {

            // Nome da tabela
            builder.ToTable("tb_insumoestruturado");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_insumopmo");

            builder.Property(t => t.QuantidadeMesesAdiante)
                   .HasColumnName("qtd_mesesadiante");

            builder.Property(t => t.TipoBloco)
                   .HasColumnName("tip_blocomontador");

            builder.Property(t => t.OrdemBlocoMontador)
                   .HasColumnName("num_ordemblocomontador");

            builder.Property(t => t.CategoriaInsumoId)
                   .HasColumnName("id_tpcategoriainsumo");

            builder.Property(t => t.TipoColetaId)
                   .HasColumnName("id_tpcoleta");

            // Relacionamento com CategoriaInsumo
            builder.HasOne(t => t.CategoriaInsumo)
                   .WithMany()
                   .HasForeignKey(t => t.CategoriaInsumoId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            // Relacionamento com TipoColeta
            builder.HasOne(t => t.TipoColeta)
                   .WithMany()
                   .HasForeignKey(t => t.TipoColetaId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional
        }
    }
}
