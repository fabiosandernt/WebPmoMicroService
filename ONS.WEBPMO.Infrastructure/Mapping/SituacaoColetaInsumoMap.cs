using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class SituacaoColetaInsumoMap : IEntityTypeConfiguration<SituacaoColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<SituacaoColetaInsumo> builder)
        {
            throw new NotImplementedException();
        }
    }
}
