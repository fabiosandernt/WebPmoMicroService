using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaEstruturadoMap : IEntityTypeConfiguration<DadoColetaEstruturado>
    {
        public void Configure(EntityTypeBuilder<DadoColetaEstruturado> builder)
        {
            throw new NotImplementedException();
        }
    }
}