using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    public class UsinaMap : IEntityTypeConfiguration<Usina>
    {

        public void Configure(EntityTypeBuilder<Usina> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_aux_usina");



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

            builder.Property(t => t.TipoUsina)
                   .HasColumnName("cod_tpgeracao")
                   .HasMaxLength(15)
                   .IsRequired();

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