using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class ParametroMap : IEntityTypeConfiguration<Parametro>
    {
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            throw new NotImplementedException();
        }
    }
}