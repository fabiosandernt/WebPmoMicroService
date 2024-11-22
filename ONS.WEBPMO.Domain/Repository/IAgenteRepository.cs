﻿using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{
    public interface IAgenteRepository : IRepository<Agente>
    {
        ICollection<Agente> ConsultarAgentesGabaritoPaginado(GabaritoParticipantesFilter filter);

        IList<Agente> ConsultarAgentesGabarito(GabaritoParticipantesFilter filter);

        IList<Agente> ConsultarPorNome(string nome);

        IList<Agente> ConsultarAgentesGabarito(int? idSemanaOperativa, IList<int> containIdsAgente);
        List<Agente> ObterAgentesPorIds(IList<int> idsAgente);
    }
}
