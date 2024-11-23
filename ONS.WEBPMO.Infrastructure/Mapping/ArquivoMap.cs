using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ArquivoMap : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_arquivo");

            // Relacionamento 1:1
            builder.HasOne(arquivo => arquivo.Content)
                   .WithOne()
                   .HasForeignKey<Entities.PMO.BinaryData>(binaryData => binaryData.Id);

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_arquivo");

            builder.Property(t => t.Nome)
                   .HasColumnName("nom_arquivo")
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(t => t.Tamanho)
                   .HasColumnName("num_tamanhoarquivo")
                   .IsRequired();

            builder.Property(t => t.MimeType)
                   .HasColumnName("dsc_mimearquivo")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.HashVerificacao)
                   .HasColumnName("cod_hashverificacao")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Deleted)
                   .HasColumnName("flg_excluido");
        }
    }
    }

