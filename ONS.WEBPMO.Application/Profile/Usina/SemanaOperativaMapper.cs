
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class SemanaOperativaMapper : AutoMapper.Profilefile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.SemanaOperativa, SemanaOperativa>();

            base.Configure();
        }
    }
}