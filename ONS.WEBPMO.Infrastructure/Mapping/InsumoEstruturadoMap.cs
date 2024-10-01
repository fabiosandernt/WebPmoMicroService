using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class InsumoEstruturadoMap : IEntityTypeConfiguration<InsumoEstruturado>
    {
        public void Configure(EntityTypeBuilder<InsumoEstruturado> builder)
        {
            throw new NotImplementedException();
        }
    }
}