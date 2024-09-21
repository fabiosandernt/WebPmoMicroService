using AutoMapper;

namespace ONS.WEBPMO.WebSite.AutoMapper
{
    public class LogNotificacaoMapper : Profile
    {
        protected override void Configure()
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