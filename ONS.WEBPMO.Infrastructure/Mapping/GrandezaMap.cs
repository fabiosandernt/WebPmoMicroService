namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    internal class GrandezaMap : EntityTypeConfiguration<Grandeza>
    {
        public GrandezaMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_grandeza");
            Property(t => t.Id).HasColumnName("id_grandeza");
            Property(t => t.Nome).HasColumnName("nom_grandeza").IsRequired().HasMaxLength(150);
            Property(t => t.OrdemExibicao).HasColumnName("num_ordemexibicao");
            Property(t => t.IsColetaPorPatamar).HasColumnName("flg_coletaporpatamar");
            Property(t => t.IsColetaPorLimite).HasColumnName("flg_coletaporlimite");
            Property(t => t.IsColetaPorEstagio).HasColumnName("flg_coletaporestagio");
            Property(t => t.AceitaValorNegativo).HasColumnName("flg_aceitavalornegativo");
            Property(t => t.PodeRecuperarValor).HasColumnName("flg_poderecuperarvalor");
            Property(t => t.DestacaDiferenca).HasColumnName("flg_destacadiferenca");
            Property(t => t.IsObrigatorio).HasColumnName("flg_obrigatoriedade");
            Property(t => t.Ativo).HasColumnName("flg_ativo");
            Property(t => t.QuantidadeCasasInteira).HasColumnName("qtd_digitos");
            Property(t => t.QuantidadeCasasDecimais).HasColumnName("qtd_decimais");
            Property(t => t.ParticipaBlocoMontador).HasColumnName("flg_participablocomontador");
            Property(t => t.OrdemBlocoMontador).HasColumnName("num_ordemblocomontador");
            Property(t => t.IsPreAprovadoComAlteracao).HasColumnName("flg_preaprovadocomalteracao");

            Property(t => t.TipoDadoGrandezaId).HasColumnName("id_tpdadograndeza");
            HasRequired(t => t.TipoDadoGrandeza).WithMany().HasForeignKey(t => t.TipoDadoGrandezaId);

            Property(t => t.InsumoId).HasColumnName("id_insumopmo");
            HasRequired(t => t.Insumo).WithMany(t => t.Grandezas).HasForeignKey(t => t.InsumoId);

        }
    }
}