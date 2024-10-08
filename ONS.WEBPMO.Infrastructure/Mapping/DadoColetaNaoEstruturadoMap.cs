using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaNaoEstruturadoMap : IEntityTypeConfiguration<DadoColetaNaoEstruturado>
    {
        public void Configure(EntityTypeBuilder<DadoColetaNaoEstruturado> builder)
        {
            
            builder.ToTable("tb_dadocoletanaoestruturado");
            builder.Property(t => t.Id).HasColumnName("id_dadocoleta");
            builder.Property(t => t.Observacao).HasColumnName("obs_dadocoletanaoestruturado").HasMaxLength(1000);

            builder.HasMany(t => t.Arquivos)
                  .WithMany() // Não existe a propriedade no outro lado
                  .UsingEntity<Dictionary<string, object>>(
                       "tb_arqdadocoletanaoestruturado", // Nome da tabela de junção
                       j => j.HasOne<Arquivo>().WithMany().HasForeignKey("id_arquivo"), // Configuração do lado de "Arquivo"
                       j => j.HasOne<DadoColetaNaoEstruturado>().WithMany().HasForeignKey("id_dadocoleta"), // Configuração do lado de "DadoColetaNaoEstruturado"
                       j =>
                       {
                           j.HasKey("id_dadocoleta", "id_arquivo"); // Chave composta
                       });
        }
    }
    
}