
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class PMOMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.PMO, PMO>();

            base.Configure();
        }
    }
}