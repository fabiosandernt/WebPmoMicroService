using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class GrandezaMap : IEntityTypeConfiguration<Grandeza>
    {
        public void Configure(EntityTypeBuilder<Grandeza> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("tb_grandeza");

            builder.Property(t => t.Id).HasColumnName("id_grandeza");
            builder.Property(t => t.Nome).HasColumnName("nom_grandeza").IsRequired().HasMaxLength(150);
            builder.Property(t => t.OrdemExibicao).HasColumnName("num_ordemexibicao");
            builder.Property(t => t.IsColetaPorPatamar).HasColumnName("flg_coletaporpatamar");
            builder.Property(t => t.IsColetaPorLimite).HasColumnName("flg_coletaporlimite");
            builder.Property(t => t.IsColetaPorEstagio).HasColumnName("flg_coletaporestagio");
            builder.Property(t => t.AceitaValorNegativo).HasColumnName("flg_aceitavalornegativo");
            builder.Property(t => t.PodeRecuperarValor).HasColumnName("flg_poderecuperarvalor");
            builder.Property(t => t.DestacaDiferenca).HasColumnName("flg_destacadiferenca");
            builder.Property(t => t.IsObrigatorio).HasColumnName("flg_obrigatoriedade");
            builder.Property(t => t.Ativo).HasColumnName("flg_ativo");
            builder.Property(t => t.QuantidadeCasasInteira).HasColumnName("qtd_digitos");
            builder.Property(t => t.QuantidadeCasasDecimais).HasColumnName("qtd_decimais");
            builder.Property(t => t.ParticipaBlocoMontador).HasColumnName("flg_participablocomontador");
            builder.Property(t => t.OrdemBlocoMontador).HasColumnName("num_ordemblocomontador");
            builder.Property(t => t.IsPreAprovadoComAlteracao).HasColumnName("flg_preaprovadocomalteracao");

            builder.Property(t => t.TipoDadoGrandezaId).HasColumnName("id_tpdadograndeza");
            builder.HasOne(t => t.TipoDadoGrandeza).WithMany().HasForeignKey(t => t.TipoDadoGrandezaId);

            builder.Property(t => t.InsumoId).HasColumnName("id_insumopmo");
            builder.HasOne(t => t.Insumo).WithMany(t => t.Grandezas).HasForeignKey(t => t.InsumoId);
        }
    }
}
