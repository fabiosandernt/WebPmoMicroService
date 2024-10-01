using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Models.Mapping
{
    public class TipoDadoGrandezaMap : IEntityTypeConfiguration<TipoDadoGrandeza>
    {
        public void Configure(EntityTypeBuilder<TipoDadoGrandeza> builder)
        {
            throw new NotImplementedException();
        }
    }
}
