using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoConvergenciaMap : IEntityTypeConfiguration<DadoConvergencia>
    {
        public void Configure(EntityTypeBuilder<DadoConvergencia> builder)
        {
            throw new NotImplementedException();
        }
    }
}
