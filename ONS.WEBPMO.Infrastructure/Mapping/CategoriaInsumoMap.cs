using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class CategoriaInsumoMap : IEntityTypeConfiguration<CategoriaInsumo>
    {
        public void Configure(EntityTypeBuilder<CategoriaInsumo> builder)
        {
            throw new NotImplementedException();
        }
    }
}
