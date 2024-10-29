using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class CategoriaInsumoMap : IEntityTypeConfiguration<CategoriaInsumo>
    {
        public void Configure(EntityTypeBuilder<CategoriaInsumo> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_tb_tpcategoriainsumo");

            builder.ToTable("tb_tpcategoriainsumo");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id_tpcategoriainsumo");
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("dsc_tpcategoriainsumo");
            builder.Property(e => e.UsoMontador).HasColumnName("flg_montador");
            builder.Property(e => e.UsoPmo).HasColumnName("flg_pmo");
        }
    }
}
