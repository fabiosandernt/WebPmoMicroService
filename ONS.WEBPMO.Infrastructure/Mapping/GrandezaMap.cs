using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class GrandezaMap : IEntityTypeConfiguration<Grandeza>
    {
        public void Configure(EntityTypeBuilder<Grandeza> builder)
        {
            throw new NotImplementedException();
        }
    }
}