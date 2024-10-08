using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.Common.Entities;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaEstruturadoMap : IEntityTypeConfiguration<DadoColetaEstruturado>
    {
        public void Configure(EntityTypeBuilder<DadoColetaEstruturado> builder)
        {
            

            builder.HasIndex(e => e.Id, "in_fk_dadocoleta_dadocoletaestruturado");
            builder.Property(t => t.Id).HasColumnName("id_dadocoleta");
            builder.Property(t => t.Valor).HasColumnName("val_dado").HasMaxLength(4000);
            builder.Property(t => t.Estagio).HasColumnName("dsc_estagio").HasMaxLength(50);
            builder.Property(t => t.TipoLimiteId).HasColumnName("id_tplimite");
            builder.HasOne(t => t.TipoLimite).WithMany().HasForeignKey(t => t.TipoLimiteId);
            builder.Property(t => t.TipoPatamarId).HasColumnName("id_tppatamar");
            builder.HasOne(t => t.TipoPatamar).WithMany().HasForeignKey(t => t.TipoPatamarId);
            builder.Property(t => t.DestacaModificacao).HasColumnName("flg_destacamodificacao");
        }
    }
}