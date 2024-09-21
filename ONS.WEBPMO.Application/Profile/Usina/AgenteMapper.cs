
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class AgenteMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.Agente, Agente>();

            base.Configure();
        }
    }
}