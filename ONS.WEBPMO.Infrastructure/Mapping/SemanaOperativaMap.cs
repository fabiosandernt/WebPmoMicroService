using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class SemanaOperativaMap : IEntityTypeConfiguration<SemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<SemanaOperativa> builder)
        {
            throw new NotImplementedException();
        }
    }
}