
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class GabaritoMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.Gabarito, Gabarito>();

            base.Configure();
        }
    }
}