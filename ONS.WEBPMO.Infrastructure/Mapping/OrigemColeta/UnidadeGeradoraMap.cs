namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{
    internal class UnidadeGeradoraMap : EntityTypeConfiguration<UnidadeGeradora>
    {
        public UnidadeGeradoraMap()
        {
            ToTable("tb_aux_unidadegeradora");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("id_origemcoleta").HasMaxLength(50).IsRequired();
            Property(t => t.PotenciaNominal).HasColumnName("val_potencianominal");
            Property(t => t.CodigoDPP).HasColumnName("cod_dpp");
            Property(t => t.NumeroConjunto).HasColumnName("num_conjunto");
            Property(t => t.NumeroMaquina).HasColumnName("num_maquina");

            Property(t => t.UsinaId).HasColumnName("id_origemcoletausinapai");
            HasRequired(t => t.Usina)
                .WithMany(t => t.UnidadesGeradoras)
                .HasForeignKey(t => t.UsinaId);
        }
    }
}