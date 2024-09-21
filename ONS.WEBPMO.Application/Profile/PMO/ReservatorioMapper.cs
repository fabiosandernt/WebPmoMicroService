
namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class ReservatorioMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<ReservatorioPMO, Reservatorio>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => rPmo.Id))
                .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeExibicao))
                .ForMember(r => r.NomeCurto, opt => opt.MapFrom(rPmo => rPmo.NomeCurto))
                .ForMember(r => r.NomeLongo, opt => opt.MapFrom(rPmo => rPmo.NomeLongo))
                .ForMember(r => r.CodigoDPP, opt => opt.MapFrom(rPmo => rPmo.Codigo))
                .ForMember(r => r.IdSubsistema, opt => opt.MapFrom(rPmo => (rPmo.SiglaSubsistema ?? string.Empty).PadRight(2)));

            base.Configure();
        }
    }
}