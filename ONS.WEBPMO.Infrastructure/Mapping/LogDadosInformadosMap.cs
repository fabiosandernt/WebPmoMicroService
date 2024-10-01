using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class LogDadosInformadosMap : IEntityTypeConfiguration<LogDadosInformados>
    {
        public void Configure(EntityTypeBuilder<LogDadosInformados> builder)
        {
            throw new NotImplementedException();
        }
    }
}