using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    
    public interface IAgenteService 
    {
        
        
        Task<Agente> ObterOuCriarAgentePorChave(int chave);
                
        
        Agente ObterAgentePorChaveOnline(int idAgente);

        
        
        IList<Agente> ConsultarAgentesPorNomeOnline(string nome);

        
        
        IList<Agente> ConsultarTodosAgentesPorNomeOnline(string nome);

        
        
        PagedResult<Agente> ConsultarAgentesParticipamGabaritoPaginado(GabaritoParticipantesFilter filter);

        
        
        IList<Agente> ConsultarAgentesParticipamGabarito(GabaritoParticipantesFilter filter);

        
        
        IList<Agente> ConsultarAgentesParticipanteGabaritoRepresentadoUsuarioLogado(int? idSemanaOperativa);

        
        
        bool IsAgenteONS(int idAgente);

        
        
        IList<Agente> ConsultarAgentesPorNome(string nomeAgente);

        
        void SincronizarAgentesComCDRE();

        
        
        List<Agente> ObterAgentesPorIds(IList<int> idsAgente);
    }
}
