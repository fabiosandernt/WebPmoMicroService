using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ColetaInsumoMap : IEntityTypeConfiguration<ColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<ColetaInsumo> builder)
        {
            throw new NotImplementedException();
        }
    }
}