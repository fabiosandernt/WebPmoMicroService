namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class LogNotificacaoMap : EntityTypeConfiguration<LogNotificacao>
    {
        public LogNotificacaoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_lognotificacao");
            Property(t => t.Id).HasColumnName("id_lognotificacao");

            Property(t => t.AgenteId).HasColumnName("id_agenteinstituicao");
            HasRequired(t => t.Agente).WithMany(t => t.LogNotificacoes).HasForeignKey(t => t.AgenteId);

            Property(t => t.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            HasRequired(t => t.SemanaOperativa).WithMany().HasForeignKey(t => t.SemanaOperativaId);

            Property(t => t.Usuario).HasColumnName("nom_usuario").HasMaxLength(150);
            Property(t => t.EmailsEnviar).HasColumnName("mail_enviar").HasMaxLength(4000);
            Property(t => t.EmailsEnviados).HasColumnName("mail_enviado").HasMaxLength(4000);
            Property(t => t.Acao).HasColumnName("dsc_acao");
            Property(t => t.DataEnvioNotificacao).HasColumnName("din_acao");
        }
    }
}
