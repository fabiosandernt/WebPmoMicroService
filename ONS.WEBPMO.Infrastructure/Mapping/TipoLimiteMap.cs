using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoLimiteMap : IEntityTypeConfiguration<TipoLimite>
    {
        public void Configure(EntityTypeBuilder<TipoLimite> builder)
        {
            // Nome da tabela
            builder.ToTable("tb_tplimite");

            // Chave primária
            builder.HasKey(t => t.Id);

            // Configuração das propriedades
            builder.Property(t => t.Id)
                   .HasColumnName("id_tplimite")
                   .ValueGeneratedNever(); // Equivalente a HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)

            builder.Property(t => t.Descricao)
                   .HasColumnName("dsc_tplimite")
                   .IsRequired()
                   .HasMaxLength(20);
        }
    }
}
