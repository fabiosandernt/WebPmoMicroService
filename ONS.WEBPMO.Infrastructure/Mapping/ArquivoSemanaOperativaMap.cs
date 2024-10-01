using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ArquivoSemanaOperativaMap : IEntityTypeConfiguration<ArquivoSemanaOperativa>
    {
        

        public void Configure(EntityTypeBuilder<ArquivoSemanaOperativa> builder)
        {
            builder.HasKey(e => e.Id).HasName("pk_tb_arquivosemanaoperativa");

            builder.ToTable("tb_arquivosemanaoperativa", tb => tb.HasComment("Associação de arquivos encaminhados por semana operativa "));

            builder.HasIndex(e => e.ArquivoId, "in_fk_arquivo_arquivosemanaoperativa").HasFillFactor(90);

            builder.HasIndex(e => e.SemanaOperativaId, "in_fk_semanaoperativa_arquivosemanaoperativa").HasFillFactor(90);

            builder.HasIndex(e => e.SituacaoId, "in_fk_tpsituacaosemanaoper_arquivosemanaoperativa").HasFillFactor(90);

            builder.HasIndex(e => e.Id, "in_pk_tb_arquivosemanaoperativa")
                .IsUnique()
                .HasFillFactor(90);

            builder.Property(e => e.Id)
                .HasComment("Identificador do arquivo da semana operativa")
                .HasColumnName("id_arquivosemanaoperativa");
            builder.Property(e => e.IsPublicado)
                .HasComment("Indica se o arquivo associado a semana operativa foi publicado")
                .HasColumnName("flg_publicado");
            builder.Property(e => e.ArquivoId)
                .HasComment("Identificador dos arquivos que contém insumos para o sistema")
                .HasColumnName("id_arquivo");
            builder.Property(e => e.SemanaOperativaId)
                .HasComment("Identificadorda semana operativa do programa mensal de operação - PMO")
                .HasColumnName("id_semanaoperativa");
            builder.Property(e => e.SituacaoId)
                .HasComment("Identificador da situação da semana operativado. Possíveis valores: Configuração, Coleta de dados, Geração de blocos, Convergência CCEE e Publicado")
                .HasColumnName("id_tpsituacaosemanaoper");

            builder.HasOne(d => d.Arquivo).WithMany(p => p.ArquivosSemanaOperativas)
                .HasForeignKey(d => d.ArquivoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_arquivo_arquivosemanaoperativa");

            builder.HasOne(d => d.SemanaOperativa).WithMany(p => p.Arquivos)
                .HasForeignKey(d => d.SemanaOperativaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_semanaoperativa_arquivosemanaoperativa");

            builder.HasOne(d => d.Situacao).WithMany(p => p.ArquivosSemanaOperativas)
                .HasForeignKey(d => d.SituacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tpsituacaosemanaoper_arquivosemanaoperativa");
        }
    }
}
