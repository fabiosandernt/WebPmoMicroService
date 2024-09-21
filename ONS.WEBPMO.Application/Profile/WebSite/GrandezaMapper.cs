using AutoMapper;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.WebSite.Models;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class GrandezaMapper : Profile
    {
        protected override void Configure()
        {
            CreateMap<ManutencaoGrandezaModel, Grandeza>()
                .ForMember(model => model.TipoDadoGrandeza, opt => opt.ResolveUsing( model => new TipoDadoGrandeza()
                    {
                        Id = model.TipoDadoGrandezaId,
                        Descricao = model.TipoDadoGrandezaDescricao
                    }))
                .ForMember(model => model.Insumo, opt => opt.ResolveUsing(model => new Insumo()
                    {
                        Id = model.InsumoId
                    }));

            base.Configure();
        }
    }
}