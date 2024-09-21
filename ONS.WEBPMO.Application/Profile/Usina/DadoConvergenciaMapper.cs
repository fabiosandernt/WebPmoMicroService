using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class DadoConvergenciaMapper : AutoMapper.Profile
    {
        public DadoConvergenciaMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.DadoConvergencia, DadoConvergencia>();


        }
    }
}