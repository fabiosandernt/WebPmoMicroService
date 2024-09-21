
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ColetaInsumoMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.ColetaInsumo, ColetaInsumo>();

            base.Configure();
        }
    }
}