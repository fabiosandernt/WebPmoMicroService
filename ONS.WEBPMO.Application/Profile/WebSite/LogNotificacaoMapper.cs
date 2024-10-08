using AutoMapper;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Models.LogNotificacao;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class LogNotificacaoMapper : Profile
    {
        public LogNotificacaoMapper()
        {
            CreateMap<LogNotificacaoDTO, PesquisaLogNotificacaoModel>();

            CreateMap<PesquisaLogNotificacaoModel, LogNotificacaoFilter>()
                .ForMember(p => p.Abertura,
                m => m.MapFrom(l => l.IdsAcoes.Contains((int)AcaoLogNotificacaoEnum.Abertura)))

                .ForMember(p => p.Reabertura,
                m => m.MapFrom(l => l.IdsAcoes.Contains((int)AcaoLogNotificacaoEnum.Reabertura)))

                .ForMember(p => p.Rejeicao,
                m => m.MapFrom(l => l.IdsAcoes.Contains((int)AcaoLogNotificacaoEnum.Rejeicao)));
        }
    }
}