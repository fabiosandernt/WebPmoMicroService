using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;
using ONS.WEBPMO.Infrastructure.DataBase;

namespace ONS.WEBPMO.Domain.Repositories.Impl
{


    public class AgenteRepository : Repository<Agente>, IAgenteRepository
    {

        private IQueryable<Agente> MontarQueryAgentesGabarito(GabaritoParticipantesFilter filter)
        {
            IQueryable<Agente> agentes = this.EntitySet.Where(agente => agente.Gabaritos.Any(gabarito => gabarito.IsPadrao == filter.IsPadrao));
            if (filter.IdAgente.HasValue)
            {
                agentes = agentes.Where(agente => agente.Id == filter.IdAgente.Value);
            }
            if (filter.IdSemanaOperativa.HasValue)
            {
                agentes = agentes.Where(agente => agente.Gabaritos.Any(gabarito => gabarito.SemanaOperativa.Id == filter.IdSemanaOperativa));
            }
            if (!String.IsNullOrWhiteSpace(filter.CodigoPerfilONS))
            {
                agentes = agentes.Where(agente => agente.Gabaritos.Any(gabarito => gabarito.CodigoPerfilONS == filter.CodigoPerfilONS));
            }
            return agentes;
        }

        public IList<Agente> ConsultarAgentesGabarito(GabaritoParticipantesFilter filter)
        {
            IQueryable<Agente> agentes = MontarQueryAgentesGabarito(filter);

            return agentes.ToList();
        }

        public IList<Agente> ConsultarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                int quantidadeMaxima = ONSConfigurationManager.GetSettings(
                    ONSConfigurationManager.ConfigNameMaxResultsAutoComplete, 10);

                return EntitySet.OrderBy(a => a.Nome).Take(quantidadeMaxima).ToList();
            }

            return EntitySet.Where(a => a.Nome.ToLower().Contains(nome.ToLower()))
                .OrderBy(a => a.Nome).Take(10).ToList();
        }

        public IList<Agente> ConsultarAgentesGabarito(int? idSemanaOperativa, IList<int> containIdsAgente)
        {
            var query = Context.Set<Gabarito>().AsQueryable();

            if (idSemanaOperativa.HasValue)
            {
                query = query.Where(g => g.SemanaOperativa.Id == idSemanaOperativa);
            }

            query = query.Where(g => containIdsAgente.Contains(g.Agente.Id));

            return query
                .Select(g => g.Agente)
                .Distinct()
                .ToList();
        }

        //public PagedResult<Agente> ConsultarAgentesGabaritoPaginado(GabaritoParticipantesFilter filter)
        //{
        //    IQueryable<Agente> agentes = MontarQueryAgentesGabarito(filter);

        //    return this.FindPaged(agentes, filter.PageIndex, filter.PageSize, ages => ages.OrderBy(agente => agente.Nome));
        //}

        public List<Agente> ObterAgentesPorIds(IList<int> idsAgente)
        {
            var query = EntitySet.AsQueryable();
            return query.Where(agente => idsAgente.Contains(agente.Id)).ToList();
        }
    }
}
