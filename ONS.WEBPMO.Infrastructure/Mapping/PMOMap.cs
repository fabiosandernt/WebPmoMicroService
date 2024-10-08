using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class PMOMap : IEntityTypeConfiguration<PMO>
    {
        public void Configure(EntityTypeBuilder<PMO> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("tb_pmo");

            builder.Property(t => t.Id).HasColumnName("id_pmo");
            builder.Property(t => t.AnoReferencia).HasColumnName("ano_referencia");
            builder.Property(t => t.MesReferencia).HasColumnName("mes_referencia");
            builder.Property(t => t.QuantidadeMesesAdiante).HasColumnName("qtd_mesesadiante");

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsRowVersion();

            builder.HasMany(e => e.SemanasOperativas)
                   .WithOne(e => e.PMO)
                   .HasForeignKey(e => e.PMOId)
                   .IsRequired();
        }
    }
}