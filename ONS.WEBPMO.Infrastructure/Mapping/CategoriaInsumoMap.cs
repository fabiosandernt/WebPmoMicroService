using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class CategoriaInsumoMap : IEntityTypeConfiguration<CategoriaInsumo>
    {
        public void Configure(EntityTypeBuilder<CategoriaInsumo> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_tpcategoriainsumo");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_tpcategoriainsumo")
                   .ValueGeneratedNever(); // Equivalente ao DatabaseGeneratedOption.None

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tpcategoriainsumo")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.UsoPmo)
                   .HasColumnName("flg_pmo")
                   .IsRequired();

            builder.Property(t => t.UsoMontador)
                   .HasColumnName("flg_montador")
                   .IsRequired();

            // Propriedade TipoUsina removida conforme e-mail de 29/08/2018 às 10:40h
        }
    }
}
