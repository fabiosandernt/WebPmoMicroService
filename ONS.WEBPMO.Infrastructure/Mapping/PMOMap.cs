using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class PMOMap : IEntityTypeConfiguration<PMO>
    {
        public void Configure(EntityTypeBuilder<PMO> builder)
        { // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_pmo");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_pmo");

            builder.Property(t => t.AnoReferencia)
                   .HasColumnName("ano_referencia");

            builder.Property(t => t.MesReferencia)
                   .HasColumnName("mes_referencia");

            builder.Property(t => t.QuantidadeMesesAdiante)
                   .HasColumnName("qtd_mesesadiante");

            builder.Property(t => t.Versao)
                   .HasColumnName("ver_controleconcorrencia")
                   .IsRowVersion();

            // Relacionamento com SemanasOperativas
            builder.HasMany(e => e.SemanasOperativas)
                   .WithOne(e => e.PMO)
                   .HasForeignKey(e => e.PMOId)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao WithRequired
        }
    }
}