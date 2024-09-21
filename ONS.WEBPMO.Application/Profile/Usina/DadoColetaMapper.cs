
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class DadoColetaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.DadoColeta, DadoColeta>();

            base.Configure();
        }
    }
}