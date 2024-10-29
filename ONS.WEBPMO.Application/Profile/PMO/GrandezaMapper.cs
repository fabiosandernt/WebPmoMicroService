
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class GrandezaMapper : AutoMapper.Profile
    {
        public GrandezaMapper()
        {
            CreateMap<ManutencaoGrandezaDTO, Grandeza>();
        }


    }
}