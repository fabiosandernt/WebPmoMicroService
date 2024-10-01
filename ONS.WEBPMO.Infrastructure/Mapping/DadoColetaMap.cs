using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaMap : IEntityTypeConfiguration<DadoColeta>
    {
        public void Configure(EntityTypeBuilder<DadoColeta> builder)
        {
            throw new NotImplementedException();
        }
    }
}