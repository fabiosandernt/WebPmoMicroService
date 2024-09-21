using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class CategoriaInsumoMap : EntityTypeConfiguration<CategoriaInsumo>
    {
        public CategoriaInsumoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("tb_tpcategoriainsumo");
            Property(t => t.Id).HasColumnName("id_tpcategoriainsumo").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Descricao).HasColumnName("dsc_tpcategoriainsumo").IsRequired().HasMaxLength(20);
            Property(t => t.UsoPmo).HasColumnName("flg_pmo").IsRequired();
            Property(t => t.UsoMontador).HasColumnName("flg_montador").IsRequired();
            //Property(t => t.TipoUsina).HasColumnName("id_tpusina").HasMaxLength(3); ; Retirado, conforme solicitação de e-mail enviado em 29/08/2018 10:40h

        }
    }
}
