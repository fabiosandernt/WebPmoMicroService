using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

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
