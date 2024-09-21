
using ONS.WEBPMO.Domain.Entities.Usina;

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ArquivoSemanaMapper : AutoMapper.Profile
    {
        public ArquivoSemanaMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.ArquivoSemanaOperativa, ArquivoSemanaOperativa>();
        }
       
    }
}