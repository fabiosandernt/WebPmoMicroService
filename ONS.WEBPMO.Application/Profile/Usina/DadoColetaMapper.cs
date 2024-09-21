using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class DadoColetaMapper : AutoMapper.Profile
    {
        public DadoColetaMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.DadoColeta, DadoColeta>();
        }

    }
}