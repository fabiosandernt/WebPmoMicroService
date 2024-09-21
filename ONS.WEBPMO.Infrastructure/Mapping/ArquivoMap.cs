namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Entities;

    internal class ArquivoMap : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_arquivo");
            HasRequired(arquivo => arquivo.Content).WithRequiredPrincipal();
            Property(t => t.Id).HasColumnName("id_arquivo");
            Property(t => t.Nome).HasColumnName("nom_arquivo").IsRequired().HasMaxLength(255);
            Property(t => t.Tamanho).HasColumnName("num_tamanhoarquivo").IsRequired();
            Property(t => t.MimeType).HasColumnName("dsc_mimearquivo").IsRequired().HasMaxLength(100);
            Property(t => t.HashVerificacao).HasColumnName("cod_hashverificacao").IsRequired().HasMaxLength(100);
            Property(t => t.Deleted).HasColumnName("flg_excluido");
        }
    }
    internal class BinaryDataMap : EntityTypeConfiguration<BinaryData>
    {
        public BinaryDataMap()
        {
            HasKey(t => t.Id);
            ToTable("tb_arquivo");
            Property(t => t.Id).HasColumnName("id_arquivo");
            Property(t => t.Data).HasColumnName("arq_conteudo").IsRequired();
        }
    }
}