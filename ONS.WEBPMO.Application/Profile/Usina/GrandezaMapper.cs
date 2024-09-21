
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class GrandezaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ManutencaoGrandezaDTO, ONS.SGIPMO.Domain.Entities.Grandeza>();
            CreateMap<ONS.SGIPMO.Domain.Entities.Grandeza, Grandeza>();

            base.Configure();
        }
    }
}