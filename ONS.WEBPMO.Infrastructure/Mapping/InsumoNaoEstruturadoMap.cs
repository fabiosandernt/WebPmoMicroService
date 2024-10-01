using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class InsumoNaoEstruturadoMap : IEntityTypeConfiguration<InsumoNaoEstruturado>
    {
        public void Configure(EntityTypeBuilder<InsumoNaoEstruturado> builder)
        {
            throw new NotImplementedException();
        }
    }
}