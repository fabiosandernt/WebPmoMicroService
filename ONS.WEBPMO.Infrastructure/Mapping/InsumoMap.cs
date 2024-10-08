using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class InsumoMap : IEntityTypeConfiguration<Insumo>
    {
        public void Configure(EntityTypeBuilder<Insumo> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("tb_insumopmo");

            builder.Property(t => t.Id).HasColumnName("id_insumopmo");
            builder.Property(t => t.Nome)
                   .HasColumnName("nom_insumopmo")
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(t => t.OrdemExibicao).HasColumnName("num_ordemexibicao");
            builder.Property(t => t.PreAprovado).HasColumnName("flg_preaprovado");
            builder.Property(t => t.Reservado).HasColumnName("flg_reservado");
            builder.Property(t => t.DataUltimaAtualizacao).HasColumnName("din_ultimaalteracao");
            builder.Property(t => t.Ativo).HasColumnName("flg_ativo");

            builder.Property(t => t.ExportarInsumo).HasColumnName("flg_exportainsumo");
            builder.Property(t => t.SiglaInsumo)
                   .HasColumnName("sgl_insumo")
                   .IsRequired()
                   .HasMaxLength(10);

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsConcurrencyToken()
                   .IsRowVersion();

            builder.Property(t => t.TipoInsumo)
                   .HasColumnName("tip_insumopmo")
                   .IsRequired()
                   .IsUnicode(false)
                   .IsFixedLength()
                   .HasMaxLength(1);
        }
    }
}
