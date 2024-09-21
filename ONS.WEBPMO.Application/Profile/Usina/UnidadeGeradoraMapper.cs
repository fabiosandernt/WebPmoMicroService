
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class UnidadeGeradoraMapper : AutoMapper.Profile
    {
        protected override void Configure()
        {
            CreateMap<UnidadeGeradoraPMO, ONS.SGIPMO.Domain.Entities.UnidadeGeradora>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => rPmo.Id))
                .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.Nome))
                .ForMember(r => r.CodigoDPP, opt => opt.MapFrom(rPmo => rPmo.IdDPP))
                .ForMember(r => r.NumeroConjunto, opt => opt.MapFrom(rPmo => rPmo.NumeroConjunto))
                .ForMember(r => r.NumeroMaquina, opt => opt.MapFrom(rPmo => rPmo.NumeroMaquina))
                .ForMember(r => r.PotenciaNominal, opt => opt.MapFrom(rPmo => rPmo.PotenciaNominal))
                .ForMember(r => r.Usina, opt => opt.MapFrom(rPmo => rPmo.UsinaPMO))
                .ForMember(r => r.Gruge_id, opt => opt.MapFrom(rPmo => rPmo.Gruge_id))
                .ForMember(r => r.Cod_subsistemamodenerg, opt => opt.MapFrom(rPmo => rPmo.Cod_subsistemamodenerg))
                .ForMember(r => r.Val_potcalcindisp, opt => opt.MapFrom(rPmo => rPmo.Val_potcalcindisp))
                .ForMember(r => r.Cod_tppotcalcindisp, opt => opt.MapFrom(rPmo => rPmo.Cod_tppotcalcindisp))
                .ForMember(r => r.Din_fim, opt => opt.MapFrom(rPmo => rPmo.Din_fim))
                .ForMember(r => r.Age_id_oper, opt => opt.MapFrom(rPmo => rPmo.Age_id_oper));

            CreateMap<ONS.SGIPMO.Domain.Entities.UnidadeGeradora, UnidadeGeradora>();

            base.Configure();
        }
    }
}