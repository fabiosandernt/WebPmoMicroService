using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class AgenteService : IAgenteService
    {
        private readonly IAgenteRepository agenteRepository;
        private readonly IParametroService parametroService;

        private readonly IBDTPMOService bdtService;

        public AgenteService(
            IAgenteRepository agenteRepository,
            IParametroService parametroService,
            IBDTPMOService bdtService)
        {
            this.agenteRepository = agenteRepository;
            this.parametroService = parametroService;
            this.bdtService = bdtService;
        }

        public IList<Agente> ConsultarAgentesParticipamGabarito(GabaritoParticipantesFilter filter)
        {
            throw new NotImplementedException();
        }

        public ICollection<Agente> ConsultarAgentesParticipamGabaritoPaginado(GabaritoParticipantesFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarAgentesParticipanteGabaritoRepresentadoUsuarioLogado(int? idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarAgentesPorNome(string nomeAgente)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarAgentesPorNomeOnline(string nome)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarTodosAgentesPorNomeOnline(string nome)
        {
            throw new NotImplementedException();
        }

        public bool IsAgenteONS(int idAgente)
        {
            throw new NotImplementedException();
        }

        public Agente ObterAgentePorChaveOnline(int idAgente)
        {
            throw new NotImplementedException();
        }

        public List<Agente> ObterAgentesPorIds(IList<int> idsAgente)
        {
            throw new NotImplementedException();
        }

        public Agente ObterOuCriarAgentePorChave(int chave)
        {
            throw new NotImplementedException();
        }

        public void SincronizarAgentesComCDRE()
        {
            throw new NotImplementedException();
        }
    }
}
