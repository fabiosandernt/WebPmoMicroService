
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class SemanaOperativaMapper : AutoMapper.Profile
    {
        public SemanaOperativaMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.SemanaOperativa, ONS.WEBPMO.Domain.Entities.Usina.SemanaOperativa>();
            
        }
    }
}