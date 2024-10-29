using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_tb_arquivo");

            builder.ToTable("tb_arquivo", tb => tb.HasComment("Tem o propósito geral de armazena dados sobre um arquivo, que poderá ser referenciado por outras dados do sistema."));

            builder.HasIndex(e => e.Id, "in_pk_tb_arquivo")
                .IsUnique()
                .HasFillFactor(90);

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Identificador dos arquivos que contém insumos para o sistema")
                .HasColumnName("id_arquivo");
            builder.Property(e => e.Content)
                .IsRequired()
                .HasComment("Conteúdo do arquivo")
                .HasColumnName("arq_conteudo");
            builder.Property(e => e.HashVerificacao)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Código Hash de verificação do arquivo (uso interno) para garantir a consistencia do arquivo")
                .HasColumnName("cod_hashverificacao");
            builder.Property(e => e.MimeType)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("Tipo do arquivo (MimeType), segundo a nomenclatura padrão da IANA")
                .HasColumnName("dsc_mimearquivo");
            builder.Property(e => e.Deleted)
                .HasDefaultValueSql("((0))")
                .HasComment("Indica se o arquivo foi excluído logicamente")
                .HasColumnName("flg_excluido");
            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(255)
                .HasComment("Nome do Arquivo com Extensão")
                .HasColumnName("nom_arquivo");
            builder.Property(e => e.Tamanho)
                .HasComment("Tamanho em bytes do arquivo")
                .HasColumnName("num_tamanhoarquivo");
        }
    }

}