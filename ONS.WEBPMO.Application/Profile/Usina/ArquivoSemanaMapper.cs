
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ArquivoSemanaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.ArquivoSemanaOperativa, ArquivoSemanaOperativa>();

            base.Configure();
        }
    }
}