using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoLimiteMap : IEntityTypeConfiguration<TipoLimite>
    {
        public void Configure(EntityTypeBuilder<TipoLimite> builder)
        {
            throw new NotImplementedException();
        }
    }
}
