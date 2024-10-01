using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class GabaritoMap : IEntityTypeConfiguration<Gabarito>
    {
        public void Configure(EntityTypeBuilder<Gabarito> builder)
        {
            throw new NotImplementedException();
        }
    }
}