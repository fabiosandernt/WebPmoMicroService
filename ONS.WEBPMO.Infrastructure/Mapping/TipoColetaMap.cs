using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoColetaMap : IEntityTypeConfiguration<TipoColeta>
    {
        public void Configure(EntityTypeBuilder<TipoColeta> builder)
        {
            // Chave prim�ria
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_tpcoleta");

            // Configura��o das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_tpcoleta")
                   .ValueGeneratedNever(); // Equivalente a HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tpcoleta")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(t => t.UsoPmo)
                   .HasColumnName("flg_pmo")
                   .IsRequired();

            builder.Property(t => t.BlocoMontador)
                   .HasColumnName("flg_blocomontador")
                   .IsRequired();

            builder.Property(t => t.MnemonicoMontador)
                   .HasColumnName("flg_mnemonicomontador")
                   .IsRequired();
        }
    }
}
