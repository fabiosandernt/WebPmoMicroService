using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class AgenteMapper : AutoMapper.Profile
    {
        public AgenteMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.Agente, Agente>();

        }
    }
}