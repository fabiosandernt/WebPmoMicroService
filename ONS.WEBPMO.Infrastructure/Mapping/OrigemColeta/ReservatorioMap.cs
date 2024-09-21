using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    public class ReservatorioMap : IEntityTypeConfiguration<Reservatorio>
    {
        public void Configure(EntityTypeBuilder<Reservatorio> builder)
        {
            builder.ToTable("tb_aux_reservatorio");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id_origemcoleta").HasMaxLength(50).IsRequired();

            builder.Property(t => t.NomeLongo).HasColumnName("nom_longo").HasMaxLength(100);
            builder.Property(t => t.NomeCurto).HasColumnName("nom_curto").HasMaxLength(50);
            builder.Property(t => t.CodigoDPP).HasColumnName("cod_dpp");
            builder.Property(e => e.IdSubsistema).HasColumnName("cod_subsistema").HasMaxLength(2);

            //HasOptional(e => e.Subsistema).WithMany().HasForeignKey(e => e.IdSubsistema).WillCascadeOnDelete(false);
        }
    }
}