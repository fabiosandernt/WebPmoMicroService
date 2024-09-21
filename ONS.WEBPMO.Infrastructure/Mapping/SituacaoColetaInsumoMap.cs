using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using Entities;
    using ONS.WEBPMO.Domain.Entities.PMO;

    public class SituacaoColetaInsumoMap : EntityTypeConfiguration<SituacaoColetaInsumo>
    {
        public SituacaoColetaInsumoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Table & Column Mappings
            ToTable("tb_tpsituacaocoletainsumo");
            Property(t => t.Id).HasColumnName("id_tpsituacaocoletainsumo").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Descricao).HasColumnName("dsc_tpsituacaocoletainsumo").IsRequired()
                .HasMaxLength(20);
        }
    }
}
