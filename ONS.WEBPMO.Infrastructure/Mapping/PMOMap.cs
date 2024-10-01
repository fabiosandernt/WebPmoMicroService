using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class PMOMap : IEntityTypeConfiguration<PMO>
    {
        public void Configure(EntityTypeBuilder<PMO> builder)
        {
            throw new NotImplementedException();
        }
    }
}