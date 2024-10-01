using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Domain.Repositories.Impl.Mapping
{
    public class DadoColetaManutencaoMap : IEntityTypeConfiguration<DadoColetaManutencao>
    {
        public void Configure(EntityTypeBuilder<DadoColetaManutencao> builder)
        {
            throw new NotImplementedException();
        }
    }
}