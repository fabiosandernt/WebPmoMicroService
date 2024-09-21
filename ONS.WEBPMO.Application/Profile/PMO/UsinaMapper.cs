
namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class UsinaMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<UsinaPMO, Usina>()
                  .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => rPmo.Id))
                  .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeExibicao))
                  .ForMember(r => r.NomeCurto, opt => opt.MapFrom(rPmo => rPmo.NomeCurto))
                  .ForMember(r => r.NomeLongo, opt => opt.MapFrom(rPmo => rPmo.NomeLongo))
                  .ForMember(r => r.CodigoDPP, opt => opt.MapFrom(rPmo => rPmo.CodUsinaPlanejamento))
                  .ForMember(r => r.IdSubsistema, opt => opt.MapFrom(rPmo => (rPmo.SiglaSubsistema ?? string.Empty).PadRight(2)))
                  .ForMember(r => r.TipoUsina,
                             opt =>
                             opt.ResolveUsing(
                                 rPmo =>
                                 rPmo.TipoGeracao.StartsWith("H")
                                     ? TipoUsinaEnum.Hidraulica.ToDescription()
                                     : TipoUsinaEnum.Termica.ToDescription()))
                  .ForMember(r => r.Cod_subsistemamodenerg, opt => opt.MapFrom(rPmo => rPmo.Cod_subsistemamodenerg))
                  .ForMember(r => r.Id_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Id_reservatorioee))
                  .ForMember(r => r.Nom_curto_reservatorioee, opt => opt.MapFrom(rPmo => rPmo.Nom_curto_reservatorioee));

            base.Configure();
        }
    }
}