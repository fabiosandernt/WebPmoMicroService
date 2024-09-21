using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class InsumoEstruturadoMapper : AutoMapper.Profile
    {
        public InsumoEstruturadoMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.InsumoEstruturado, InsumoEstruturado>();

        }
    }
}