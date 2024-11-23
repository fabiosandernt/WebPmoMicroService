using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ArquivoSemanaOperativaMap : IEntityTypeConfiguration<ArquivoSemanaOperativa>
    {


        public void Configure(EntityTypeBuilder<ArquivoSemanaOperativa> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_arquivosemanaoperativa");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_arquivosemanaoperativa");

            builder.Property(t => t.IsPublicado)
                   .HasColumnName("flg_publicado");

            builder.Property(t => t.ArquivoId)
                   .HasColumnName("id_arquivo");

            builder.Property(t => t.SemanaOperativaId)
                   .HasColumnName("id_semanaoperativa");

            builder.Property(t => t.SituacaoId)
                   .HasColumnName("id_tpsituacaosemanaoper");

            // Relacionamentos
            builder.HasOne(t => t.Arquivo)
                   .WithMany()
                   .HasForeignKey(t => t.ArquivoId)
                   .OnDelete(DeleteBehavior.Restrict); // Ajuste conforme  regra de negócio

            builder.HasOne(t => t.SemanaOperativa)
                   .WithMany()
                   .HasForeignKey(t => t.SemanaOperativaId)
                   .OnDelete(DeleteBehavior.Restrict); // Ajuste conforme  regra de negócio

            builder.HasOne(t => t.Situacao)
                   .WithMany()
                   .HasForeignKey(t => t.SituacaoId)
                   .OnDelete(DeleteBehavior.Restrict); // Ajuste conforme  regra de negócio
        }
    }
    
}
