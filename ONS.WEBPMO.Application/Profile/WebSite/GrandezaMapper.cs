using AutoMapper;
using ONS.WEBPMO.Application.Models.Insumo;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class GrandezaMapper : Profile
    {
        public GrandezaMapper()
        {
            CreateMap<ManutencaoGrandezaModel, Grandeza>()
                .ForMember(model => model.TipoDadoGrandeza, opt => opt.MapFrom(model => new TipoDadoGrandeza()
                {
                    Id = model.TipoDadoGrandezaId,
                    Descricao = model.TipoDadoGrandezaDescricao
                }))
                .ForMember(model => model.Insumo, opt => opt.MapFrom(model => new Insumo()
                {
                    Id = model.InsumoId
                }));


        }
    }
}