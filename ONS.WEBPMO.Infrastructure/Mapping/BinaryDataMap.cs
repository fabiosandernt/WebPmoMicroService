using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ONS.WEBPMO.Infrastructure.Mapping
{
    public class BinaryDataMap : IEntityTypeConfiguration<ONS.WEBPMO.Domain.Entities.PMO.BinaryData>
    {
        public void Configure(EntityTypeBuilder<ONS.WEBPMO.Domain.Entities.PMO.BinaryData> builder)
        {
            //builder.HasKey(t => t.Id);
            //builder.ToTable("tb_arquivo");
            //builder.Property(t => t.Id).HasColumnName("id_arquivo");
            //builder.Property(t => t.Data).HasColumnName("arq_conteudo").IsRequired();
        }
    }
}
