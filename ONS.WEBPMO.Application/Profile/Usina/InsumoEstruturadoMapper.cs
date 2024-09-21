namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class InsumoEstruturadoMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.InsumoEstruturado, InsumoEstruturado>();

            base.Configure();
        }
    }
}