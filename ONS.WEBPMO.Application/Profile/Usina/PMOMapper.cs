
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class PMOMapper : AutoMapper.Profile
    {
        public PMOMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.PMO, ONS.WEBPMO.Domain.Entities.Usina.PMO>();
        }
    }
}