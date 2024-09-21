using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using ONS.SGIPMO.Domain.Entities;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoPatamarMap : EntityTypeConfiguration<TipoPatamar>
    {
        public TipoPatamarMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("tb_tppatamar");
            
            Property(t => t.Id).HasColumnName("id_tppatamar").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Descricao).HasColumnName("dsc_tppatamar").IsRequired().HasMaxLength(20);
            Property(t => t.ValorVermelho).HasColumnName("val_vermelho");
            Property(t => t.ValorVerde).HasColumnName("val_verde");
            Property(t => t.ValorAzul).HasColumnName("val_azul");

        }
    }
}
