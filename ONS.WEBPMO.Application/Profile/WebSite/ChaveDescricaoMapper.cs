using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using System.Web.Mvc;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class ChaveDescricaoMapper : Profile
    {
        public ChaveDescricaoMapper()
        {
            CreateMap<ChaveDescricaoDTO<int>, SelectListItem>()
               .ForMember(e => e.Value, a => a.MapFrom(e => e.Chave))
               .ForMember(e => e.Text, a => a.MapFrom(e => e.Descricao));

            CreateMap<ChaveDescricaoDTO<string>, SelectListItem>()
                .ForMember(e => e.Value, a => a.MapFrom(e => e.Chave))
                .ForMember(e => e.Text, a => a.MapFrom(e => e.Descricao));
        }

    }
}