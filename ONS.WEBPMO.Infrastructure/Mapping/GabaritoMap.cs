using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class GabaritoMap : IEntityTypeConfiguration<Gabarito>
    {
        public void Configure(EntityTypeBuilder<Gabarito> builder)
        {
            // Definindo a chave primária
            builder.HasKey(t => t.Id);

            // Mapeando a tabela
            builder.ToTable("tb_gabarito");

            // Mapeando as propriedades
            builder.Property(t => t.Id).HasColumnName("id_gabarito");
            builder.Property(t => t.IsPadrao).HasColumnName("flg_padrao");
            builder.Property(t => t.CodigoPerfilONS)
                   .HasColumnName("cod_perfilons")
                   .HasMaxLength(30);

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsConcurrencyToken()
                   .IsRowVersion(); // Indica que é usado para controle de concorrência otimista com uma versão de linha

            // Relacionamentos
            builder.Property(t => t.AgenteId)
                   .HasColumnName("id_agenteinstituicao");

            builder.HasOne(t => t.Agente)
                   .WithMany(a => a.Gabaritos)
                   .HasForeignKey(t => t.AgenteId)
                   .IsRequired(); // Relacionamento obrigatório

            builder.Property(t => t.InsumoId)
                   .HasColumnName("id_insumopmo");

            builder.HasOne(t => t.Insumo)
                   .WithMany(i => i.Gabaritos)
                   .HasForeignKey(t => t.InsumoId)
                   .IsRequired(); // Relacionamento obrigatório

            builder.Property(t => t.OrigemColetaId)
                   .HasColumnName("id_origemcoleta");

            builder.HasOne(t => t.OrigemColeta)
                   .WithMany(o => o.Gabaritos)
                   .HasForeignKey(t => t.OrigemColetaId)
                   .IsRequired(false); // Relacionamento opcional

            builder.Property(t => t.SemanaOperativaId)
                   .HasColumnName("id_semanaoperativa");

            // Ignorar propriedade que não deve ser mapeada no banco de dados
            builder.Ignore(t => t.NomeAgentePerfil);
        }
    }
}
