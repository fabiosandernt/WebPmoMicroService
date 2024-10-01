using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ONS.WEBPMO.Infrastructure.Mapping.OrigemColeta
{

    public class OrigemColetaMap : IEntityTypeConfiguration<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta>
    {       

        public void Configure(EntityTypeBuilder<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.OrigemColeta> builder)
        {
            
        }
    }
}