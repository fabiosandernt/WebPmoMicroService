using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoColetaMap : EntityTypeConfiguration<TipoColeta>
    {
        public TipoColetaMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("tb_tpcoleta");
            Property(t => t.Id).HasColumnName("id_tpcoleta").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Descricao).HasColumnName("dsc_tpcoleta").IsRequired().HasMaxLength(20);
            Property(t => t.UsoPmo).HasColumnName("flg_pmo").IsRequired();
            Property(t => t.BlocoMontador).HasColumnName("flg_blocomontador").IsRequired();
            Property(t => t.MnemonicoMontador).HasColumnName("flg_mnemonicomontador").IsRequired();
        }
    }
}
