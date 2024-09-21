
namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class GrandezaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ManutencaoGrandezaDTO, Grandeza>();
            base.Configure();
        }
    }
}