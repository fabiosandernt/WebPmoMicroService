using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaNaoEstruturadoMap : IEntityTypeConfiguration<DadoColetaNaoEstruturado>
    {
        public void Configure(EntityTypeBuilder<DadoColetaNaoEstruturado> builder)
        {

            // Chave primária
             builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_dadocoletanaoestruturado");

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_dadocoleta");

            builder.Property(t => t.Observacao)
                   .HasColumnName("obs_dadocoletanaoestruturado")
                   .HasMaxLength(1000);

            // Relacionamento muitos-para-muitos
            builder.HasMany(t => t.Arquivos)
                   .WithMany()
                   .UsingEntity<Dictionary<string, object>>(
                       "tb_arqdadocoletanaoestruturado",
                       right => right.HasOne<Arquivo>().WithMany().HasForeignKey("id_arquivo"),
                       left => left.HasOne<DadoColetaNaoEstruturado>().WithMany().HasForeignKey("id_dadocoleta"),
                       join => join.ToTable("tb_arqdadocoletanaoestruturado"));
        }
    }
    

}