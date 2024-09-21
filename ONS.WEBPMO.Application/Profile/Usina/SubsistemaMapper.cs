
namespace ONS.WEBPMO.Application.Profile.Usina
{
    public class SubsistemaMapper : AutoMapper.Profile
    {
        public SubsistemaMapper()
        {
            CreateMap<ONS.WEBPMO.Domain.Entities.BDT.SubsistemaPMO, ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.Subsistema>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => (rPmo.Id ?? string.Empty).PadRight(2)))
                .ForMember(r => r.Codigo, opt => opt.MapFrom(rPmo => rPmo.CodigoModeloEnergia))
                .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeCurto));

            CreateMap<ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO.Subsistema, ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina.Subsistema>();

          
        }
    }
}