

namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class ArquivoMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.Arquivo, Arquivo>();

            base.Configure();
        }
    }
}