using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class GabaritoMap : IEntityTypeConfiguration<Gabarito>
    {
        public void Configure(EntityTypeBuilder<Gabarito> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_gabarito");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_gabarito");

            builder.Property(t => t.IsPadrao)
                   .HasColumnName("flg_padrao");

            builder.Property(t => t.CodigoPerfilONS)
                   .HasColumnName("cod_perfilons")
                   .HasMaxLength(30);

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsConcurrencyToken()
                   .IsRowVersion();

            builder.Property(t => t.AgenteId)
                   .HasColumnName("id_agenteinstituicao");

            builder.Property(t => t.InsumoId)
                   .HasColumnName("id_insumopmo");

            builder.Property(t => t.OrigemColetaId)
                   .HasColumnName("id_origemcoleta");

            builder.Property(t => t.SemanaOperativaId)
                   .HasColumnName("id_semanaoperativa");

            // Ignorar propriedade
            builder.Ignore(t => t.NomeAgentePerfil);

            // Relacionamentos
            builder.HasOne(t => t.Agente)
                   .WithMany(t => t.Gabaritos)
                   .HasForeignKey(t => t.AgenteId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            builder.HasOne(t => t.Insumo)
                   .WithMany(t => t.Gabaritos)
                   .HasForeignKey(t => t.InsumoId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            builder.HasOne(t => t.OrigemColeta)
                   .WithMany(t => t.Gabaritos)
                   .HasForeignKey(t => t.OrigemColetaId)
                   .OnDelete(DeleteBehavior.Restrict); // Equivalente ao HasOptional
        }
    }
}
