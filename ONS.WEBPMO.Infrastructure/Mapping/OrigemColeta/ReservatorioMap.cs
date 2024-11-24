using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    public class ReservatorioMap : IEntityTypeConfiguration<Reservatorio>
    {
        public void Configure(EntityTypeBuilder<Reservatorio> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_aux_reservatorio");


            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_origemcoleta")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(t => t.NomeLongo)
                   .HasColumnName("nom_longo")
                   .HasMaxLength(100);

            builder.Property(t => t.NomeCurto)
                   .HasColumnName("nom_curto")
                   .HasMaxLength(50);

            builder.Property(t => t.CodigoDPP)
                   .HasColumnName("cod_dpp");

            builder.Property(e => e.IdSubsistema)
                   .HasColumnName("cod_subsistema")
                   .HasMaxLength(2);

            // Relacionamento com Subsistema
            builder.HasOne(e => e.Subsistema)
                   .WithMany()
                   .HasForeignKey(e => e.IdSubsistema)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional e WillCascadeOnDelete(false)
        }
    }
}