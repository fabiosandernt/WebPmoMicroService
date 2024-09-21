
using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ArquivoMapper : AutoMapper.Profile
    {
        public ArquivoMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.Arquivo, Arquivo>();

        }
    }
}