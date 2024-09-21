namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class ParametroMap : EntityTypeConfiguration<Parametro>
    {
        public ParametroMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_parametropmo");
            Property(t => t.Id).HasColumnName("id_parametropmo");
            Property(t => t.Nome).HasColumnName("nom_parametropmo").IsRequired().HasMaxLength(50);
            Property(t => t.Valor).HasColumnName("val_parametropmo").HasMaxLength(1000);
            Property(t => t.Descricao).HasColumnName("dsc_parametropmo").HasMaxLength(255);
        }
    }
}