using System.Collections.Generic;
using System.Linq;
using ONS.SGIPMO.Domain.Entities;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Filters;
using ONS.SGIPMO.Domain.Repositories;
using System;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Domain.Presentations.Impl
{
    public class LogNotificacaoPresentation : ILogNotificacaoPresentation
    {
        private readonly IAgenteService agenteService;
        private readonly ISemanaOperativaRepository semanaOperativaRepository;

        public LogNotificacaoPresentation(IAgenteService agenteService, ISemanaOperativaRepository semanaOperativaRepository)
        {
            this.agenteService = agenteService;
            this.semanaOperativaRepository = semanaOperativaRepository;
        }
        public LogNotificacaoDTO ObterDadosPesquisaLogNotificacao(int? idSemanaOperativa = null, bool isMonitorar = false, bool? ordernarListagens = true)
        {
            LogNotificacaoDTO dadosPesquisa = new LogNotificacaoDTO();

            if (idSemanaOperativa.HasValue)
            {
                SemanaOperativa semanaOperativa = semanaOperativaRepository.FindByKey(idSemanaOperativa.Value);
                dadosPesquisa.NomeSemanaOperativaSituacao = string.Format("{0} - {1}", semanaOperativa.Nome, semanaOperativa.Situacao.Descricao);
                dadosPesquisa.SemanasOperativas.Add(new ChaveDescricaoDTO<int>(semanaOperativa.Id, semanaOperativa.Nome));
            }

            return dadosPesquisa;
        }
    }
}
