namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class DadoConvergenciaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ONS.SGIPMO.Domain.Entities.DadoConvergencia, DadoConvergencia>();

            base.Configure();
        }
    }
}