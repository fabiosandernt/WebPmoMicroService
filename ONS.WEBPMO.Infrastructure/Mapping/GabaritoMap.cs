namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class GabaritoMap : EntityTypeConfiguration<Gabarito>
    {
        public GabaritoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_gabarito");
            Property(t => t.Id).HasColumnName("id_gabarito");
            Property(t => t.IsPadrao).HasColumnName("flg_padrao");
            Property(t => t.CodigoPerfilONS).HasColumnName("cod_perfilons").HasMaxLength(30);
            Property(t => t.Versao)
                .HasColumnName("ver_controleconcorrencia")
                .IsConcurrencyToken()
                .IsRowVersion();

            // Relationships
            Property(t => t.AgenteId)
                .HasColumnName("id_agenteinstituicao");
            HasRequired(t => t.Agente)
                .WithMany(t => t.Gabaritos)
                .HasForeignKey(t => t.AgenteId);

            Property(t => t.InsumoId)
                .HasColumnName("id_insumopmo");
            HasRequired(t => t.Insumo)
                .WithMany(t => t.Gabaritos)
                .HasForeignKey(t => t.InsumoId);

            Property(t => t.OrigemColetaId)
                .HasColumnName("id_origemcoleta");
            HasOptional(t => t.OrigemColeta)
                .WithMany(t => t.Gabaritos)
                .HasForeignKey(t => t.OrigemColetaId);

            Property(t => t.SemanaOperativaId)
                .HasColumnName("id_semanaoperativa");            

            Ignore(t => t.NomeAgentePerfil);
        }
    }
}