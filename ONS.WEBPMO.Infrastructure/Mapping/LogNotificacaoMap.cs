using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class LogNotificacaoMap : IEntityTypeConfiguration<LogNotificacao>
    {
        public void Configure(EntityTypeBuilder<LogNotificacao> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_lognotificacao");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_lognotificacao");

            builder.Property(t => t.AgenteId)
                   .HasColumnName("id_agenteinstituicao");

            builder.Property(t => t.SemanaOperativaId)
                   .HasColumnName("id_semanaoperativa");

            builder.Property(t => t.Usuario)
                   .HasColumnName("nom_usuario")
                   .HasMaxLength(150);

            builder.Property(t => t.EmailsEnviar)
                   .HasColumnName("mail_enviar")
                   .HasMaxLength(4000);

            builder.Property(t => t.EmailsEnviados)
                   .HasColumnName("mail_enviado")
                   .HasMaxLength(4000);

            builder.Property(t => t.Acao)
                   .HasColumnName("dsc_acao");

            builder.Property(t => t.DataEnvioNotificacao)
                   .HasColumnName("din_acao");

            // Relacionamento com Agente
            builder.HasOne(t => t.Agente)
                   .WithMany(t => t.LogNotificacoes)
                   .HasForeignKey(t => t.AgenteId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired

            // Relacionamento com SemanaOperativa
            builder.HasOne(t => t.SemanaOperativa)
                   .WithMany()
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired
        }
    }
}
