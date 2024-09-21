namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ArquivoSemanaOperativaMap : EntityTypeConfiguration<ArquivoSemanaOperativa>
    {
        public ArquivoSemanaOperativaMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_arquivosemanaoperativa");
            Property(t => t.Id).HasColumnName("id_arquivosemanaoperativa");
            Property(t => t.IsPublicado).HasColumnName("flg_publicado");
            Property(t => t.ArquivoId).HasColumnName("id_arquivo");
            HasRequired(t => t.Arquivo).WithMany().HasForeignKey(t => t.ArquivoId);
            Property(t => t.SemanaOperativaId).HasColumnName("id_semanaoperativa");
            HasRequired(t => t.SemanaOperativa).WithMany().HasForeignKey(t => t.SemanaOperativaId);
            Property(t => t.SituacaoId).HasColumnName("id_tpsituacaosemanaoper");
            HasRequired(t => t.Situacao).WithMany().HasForeignKey(t => t.SituacaoId);
        }
    }
}
