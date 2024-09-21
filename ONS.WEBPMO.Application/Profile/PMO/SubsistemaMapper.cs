
namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class SubsistemaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<SubsistemaPMO, Subsistema>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => (rPmo.Id ?? string.Empty).PadRight(2)))
                .ForMember(r => r.Codigo, opt => opt.MapFrom(rPmo => rPmo.CodigoModeloEnergia))
                .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeCurto));

            base.Configure();
        }
    }
}