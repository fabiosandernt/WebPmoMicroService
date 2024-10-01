using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class TipoPatamarMap : IEntityTypeConfiguration<TipoPatamar>
    {
        public void Configure(EntityTypeBuilder<TipoPatamar> builder)
        {
            throw new NotImplementedException();
        }
    }
}
