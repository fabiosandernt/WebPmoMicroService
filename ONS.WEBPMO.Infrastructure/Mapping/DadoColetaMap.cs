namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class DadoColetaMap : EntityTypeConfiguration<DadoColeta>
    {
        public DadoColetaMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_dadocoleta");
            Property(t => t.Id).HasColumnName("id_dadocoleta");
            Property(t => t.TipoDadoColeta)
                .HasColumnName("tip_dadocoleta")
                .IsRequired()
                .IsUnicode(false)
                .IsFixedLength()
                .HasMaxLength(1);
            
            // Relationships
            Property(t => t.ColetaInsumoId)
                .HasColumnName("id_coletainsumo");
            HasRequired(t => t.ColetaInsumo)
                .WithMany(t => t.DadosColeta)
                .HasForeignKey(t => t.ColetaInsumoId);

            Property(t => t.GabaritoId)
                .HasColumnName("id_gabarito");
            HasRequired(t => t.Gabarito)
                .WithMany(t => t.DadosColeta)
                .HasForeignKey(t => t.GabaritoId)
                .WillCascadeOnDelete(false);

            Property(t => t.GrandezaId)
                .HasColumnName("id_grandeza");
            HasOptional(t => t.Grandeza)
                .WithMany(t => t.DadosColeta)
                .HasForeignKey(t => t.GrandezaId);
        }
    }
}