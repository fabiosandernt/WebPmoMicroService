namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class LogDadosInformadosMap : EntityTypeConfiguration<LogDadosInformados>
    {
        public LogDadosInformadosMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_logdadosinformados");
            Property(t => t.Id).HasColumnName("id_logdadosinformados");

            Property(t => t.Id_agenteinstituicao).HasColumnName("id_agenteinstituicao");
            HasRequired(t => t.Agente).WithMany(t => t.LogDadosInformados).HasForeignKey(t => t.Id_agenteinstituicao);

            Property(t => t.Id_semanaoperativa).HasColumnName("id_semanaoperativa");
            Property(t => t.Nom_usuario).HasColumnName("nom_usuario").HasMaxLength(100);
            Property(t => t.Dsc_acao).HasColumnName("dsc_acao").HasMaxLength(30);
            Property(t => t.Din_acao).HasColumnName("din_acao");
        }
    }
}