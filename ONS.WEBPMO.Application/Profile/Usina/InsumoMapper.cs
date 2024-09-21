
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class InsumoMapper : AutoMapper.Profile
    {
        public InsumoMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.Insumo, ONS.WEBPMO.Domain.Entities.Usina.Insumo>();
        }
    }
}