using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface ILogNotificacaoService 
    {
        
        //[UseNetDataContractSerializer("Agente", "SemanaOperativa")]
        PagedResult<LogNotificacao> ObterLogNotificacao(LogNotificacaoFilter filter);

        void LogarNotificacao(SemanaOperativa semanaOperativa, List<Agente> agentes, DateTime? dataHoraAcao, string nomeUsuario, string acao);

        
        bool Apagar(List<int> idsLogs);
    }
}
