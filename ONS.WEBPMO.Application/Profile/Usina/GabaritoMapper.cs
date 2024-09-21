
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class GabaritoMapper : AutoMapper.Profile
    {
        public GabaritoMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.Gabarito, ONS.WEBPMO.Domain.Entities.Usina.Gabarito>();
        }
    }
}
