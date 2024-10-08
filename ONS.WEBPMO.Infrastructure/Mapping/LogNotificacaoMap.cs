using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class LogNotificacaoMap : IEntityTypeConfiguration<LogNotificacao>
    {
        public void Configure(EntityTypeBuilder<LogNotificacao> builder)
        {
            builder.HasKey(t => t.Id);

            builder.ToTable("tb_lognotificacao");

            builder.Property(t => t.Id).HasColumnName("id_lognotificacao");

            builder.Property(t => t.AgenteId).HasColumnName("id_agenteinstituicao");
            builder.HasOne(t => t.Agente)
                   .WithMany(t => t.LogNotificacoes)
                   .HasForeignKey(t => t.AgenteId)
                   .IsRequired();

            builder.Property(t => t.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            builder.HasOne(t => t.SemanaOperativa)
                   .WithMany()
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .IsRequired();

            builder.Property(t => t.Usuario)
                   .HasColumnName("nom_usuario")
                   .HasMaxLength(150);

            builder.Property(t => t.EmailsEnviar)
                   .HasColumnName("mail_enviar")
                   .HasMaxLength(4000);

            builder.Property(t => t.EmailsEnviados)
                   .HasColumnName("mail_enviado")
                   .HasMaxLength(4000);

            builder.Property(t => t.Acao).HasColumnName("dsc_acao");

            builder.Property(t => t.DataEnvioNotificacao).HasColumnName("din_acao");
        }
    }
}
