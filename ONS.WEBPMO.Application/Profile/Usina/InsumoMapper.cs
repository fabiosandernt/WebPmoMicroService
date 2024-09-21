
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class InsumoMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.Insumo, Insumo>();

            base.Configure();
        }
    }
}