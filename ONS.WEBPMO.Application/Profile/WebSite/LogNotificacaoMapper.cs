using AutoMapper;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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