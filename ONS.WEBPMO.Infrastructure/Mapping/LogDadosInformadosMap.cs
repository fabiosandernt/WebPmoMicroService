using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class LogDadosInformadosMap : IEntityTypeConfiguration<LogDadosInformados>
    {
        public void Configure(EntityTypeBuilder<LogDadosInformados> builder)
        {
            // Chave primária
            builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_logdadosinformados");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_logdadosinformados");

            builder.Property(t => t.Id_agenteinstituicao)
                   .HasColumnName("id_agenteinstituicao");

            builder.Property(t => t.Id_semanaoperativa)
                   .HasColumnName("id_semanaoperativa");

            builder.Property(t => t.Nom_usuario)
                   .HasColumnName("nom_usuario")
                   .HasMaxLength(100);

            builder.Property(t => t.Dsc_acao)
                   .HasColumnName("dsc_acao")
                   .HasMaxLength(30);

            builder.Property(t => t.Din_acao)
                   .HasColumnName("din_acao");

            // Relacionamento com Agente
            builder.HasOne(t => t.Agente)
                   .WithMany(t => t.LogDadosInformados)
                   .HasForeignKey(t => t.Id_agenteinstituicao)
                   .OnDelete(DeleteBehavior.Cascade); // Equivalente ao HasRequired
        }
    }
}
