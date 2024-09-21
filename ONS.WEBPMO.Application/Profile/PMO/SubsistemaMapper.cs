
using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Profile.PMO
{
    public class SubsistemaMapper : AutoMapper.Profile
    {
        public SubsistemaMapper()
        {
            CreateMap<SubsistemaPMO, Subsistema>()
                .ForMember(r => r.Id, opt => opt.MapFrom(rPmo => (rPmo.Id ?? string.Empty).PadRight(2)))
                .ForMember(r => r.Codigo, opt => opt.MapFrom(rPmo => rPmo.CodigoModeloEnergia))
                .ForMember(r => r.Nome, opt => opt.MapFrom(rPmo => rPmo.NomeCurto));

           
        }
    }
}