using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class HistoricoSemanaOperativaMap : IEntityTypeConfiguration<HistoricoSemanaOperativa>
    {
        public void Configure(EntityTypeBuilder<HistoricoSemanaOperativa> builder)
        {
            throw new NotImplementedException();
        }
    }
}