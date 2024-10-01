using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class HistoricoColetaInsumoMap : IEntityTypeConfiguration<HistoricoColetaInsumo>
    {
        public void Configure(EntityTypeBuilder<HistoricoColetaInsumo> builder)
        {
            throw new NotImplementedException();
        }
    }
}