using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.Context;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{


    public class AgenteRepository : Repository<Agente>, IAgenteRepository
    {
        public AgenteRepository(WEBPMODbContext context) : base(context)
        {
        }

        public IList<Agente> ConsultarAgentesGabarito(GabaritoParticipantesFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarAgentesGabarito(int? idSemanaOperativa, IList<int> containIdsAgente)
        {
            throw new NotImplementedException();
        }

        public ICollection<Agente> ConsultarAgentesGabaritoPaginado(GabaritoParticipantesFilter filter)
        {
            throw new NotImplementedException();
        }

        public IList<Agente> ConsultarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public List<Agente> ObterAgentesPorIds(IList<int> idsAgente)
        {
            throw new NotImplementedException();
        }
    }
}
