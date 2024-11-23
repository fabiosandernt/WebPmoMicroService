using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;


namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class AgenteMap : IEntityTypeConfiguration<Agente>
    {
        public void Configure(EntityTypeBuilder<Agente> builder)
        {

            // Chave primária
             builder.HasKey(t => t.Id);

            // Nome da tabela
            builder.ToTable("tb_agenteinstituicao");

            // Propriedade Id
            builder.Property(t => t.Id)
                   .HasColumnName("id_agenteinstituicao")
                   .ValueGeneratedNever(); // Equivalente ao DatabaseGeneratedOption.None

            // Propriedade Nome
            builder.Property(t => t.Nome)
                   .HasColumnName("dsc_apelidorazaosocial")
                   .IsRequired()
                   .HasMaxLength(50);

            // Propriedade NomeLongo
            builder.Property(t => t.NomeLongo)
                   .HasColumnName("dsc_razaosocial")
                   .IsRequired()
                   .HasMaxLength(100);

        }
    }
}