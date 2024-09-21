
using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ColetaInsumoMapper : AutoMapper.Profile
    {
        public ColetaInsumoMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.ColetaInsumo, ColetaInsumo>();

        }
    }
}