namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class UsinaMap : EntityTypeConfiguration<Usina>
    {
        public UsinaMap()
        {
            ToTable("tb_aux_usina");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("id_origemcoleta").HasMaxLength(50).IsRequired();

            Property(t => t.NomeLongo).HasColumnName("nom_longo").HasMaxLength(100);
            Property(t => t.NomeCurto).HasColumnName("nom_curto").HasMaxLength(50);
            Property(t => t.CodigoDPP).HasColumnName("cod_dpp");
            Property(t => t.TipoUsina).HasColumnName("cod_tpgeracao").HasMaxLength(15).IsRequired();
            Property(e => e.IdSubsistema).HasColumnName("cod_subsistema").HasMaxLength(2);

            HasOptional(e => e.Subsistema).WithMany().HasForeignKey(e => e.IdSubsistema).WillCascadeOnDelete(false);
        }
    }
}