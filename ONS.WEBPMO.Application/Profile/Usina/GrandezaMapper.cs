using ONS.WEBPMO.Application.DTO;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class GrandezaMapper : AutoMapper.Profile
    {
        public GrandezaMapper()
        {
            CreateMap<ManutencaoGrandezaDTO, ONS.WEBPMO.Domain.Entities.PMO.Grandeza>();
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.Grandeza, ONS.WEBPMO.Domain.Entities.Usina.Grandeza>();
        }

    }
}