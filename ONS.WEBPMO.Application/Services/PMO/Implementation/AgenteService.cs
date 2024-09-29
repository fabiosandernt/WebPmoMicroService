using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Application.Services.PMO.Interfaces.Integrations;
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Enumerations;
using ONS.WEBPMO.Domain.Repository;
using System.Linq.Expressions;
using System.Reflection;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class AgenteService :  IAgenteService
    {
        private readonly IAgenteRepository agenteRepository;
        private readonly IParametroService parametroService;

        private readonly IBDTService bdtService;

        public AgenteService(
            IAgenteRepository agenteRepository,
            IParametroService parametroService,
            IBDTService bdtService)
        {
            this.agenteRepository = agenteRepository;
            this.parametroService = parametroService;
            this.bdtService = bdtService;
        }

        public PagedResult<Agente> ConsultarAgentesParticipamGabaritoPaginado(GabaritoParticipantesFilter filter)
        {
            return agenteRepository.ConsultarAgentesGabaritoPaginado(filter);
        }

        public IList<Agente> ConsultarAgentesParticipamGabarito(GabaritoParticipantesFilter filter)
        {
            return agenteRepository.ConsultarAgentesGabarito(filter);
        }

        public async Task<Agente> ObterOuCriarAgentePorChave(int chave)
        {
            var agente = await agenteRepository.GetAsync(chave);

            if (agente == null)
            {
                agente = bdtService.ConsultarAgentesPorChaves(chave).First();
                agenteRepository.SaveAsync(agente);
            }
            return agente;
        }

        public Agente ObterAgentePorChaveOnline(int idAgente)
        {
            return bdtService.ConsultarAgentesPorChaves(idAgente).First();
        }

        public IList<Agente> ConsultarTodosAgentesPorNomeOnline(string nome)
        {
            return bdtService.ConsultarAgentesPorNome(nome);
        }

        public IList<Agente> ConsultarAgentesPorNomeOnline(string nome)
        {
            return bdtService.ConsultarAgentesPorNome(nome, 10);
        }

        public IList<Agente> ConsultarAgentesPorNome(string nomeAgente)
        {
            return agenteRepository.ConsultarPorNome(nomeAgente);
        }

        public IList<Agente> ConsultarAgentesParticipanteGabaritoRepresentadoUsuarioLogado(int? idSemanaOperativa)
        {
            return agenteRepository.ConsultarAgentesGabarito(idSemanaOperativa, UserInfo.ConsultarIdsAgentesUsuarioLogado());
        }

        public bool IsAgenteONS(int idAgente)
        {
            Parametro parametro = parametroService.ObterParametro(ParametroEnum.CodigoAgenteONS);
            return int.Parse(parametro.Valor) == idAgente;
        }


        public void SincronizarAgentesComCDRE()
        {
            //obter todos os agentes do sgipmo
            var agentesSGIPMO = agenteRepository.GetAll();

            //obter agentes do cdre 
            var agentesCDRE = bdtService.ConsultarAgentesPorChaves(agentesSGIPMO.Select(ageSGIPMO => ageSGIPMO.Id).ToArray());

            //sincronizar nomes
            agentesSGIPMO.ToList().ForEach(
                ageSGIPMO =>
                    MergeAgenteComCDRE<Agente, object>(ageSGIPMO, agentesCDRE.FirstOrDefault(ageCDRE => ageCDRE.Id == ageSGIPMO.Id),
                    agente => agente.Nome,
                    agente => agente.NomeLongo));
        }

        private void MergeAgenteComCDRE<TClass, TProperty>(Agente ageSGIPMO, Agente ageCDRE, params Expression<Func<TClass, TProperty>>[] expressions)
        {
            if (ageSGIPMO != null && ageCDRE != null)
            {
                expressions.ToList().ForEach(delegate (Expression<Func<TClass, TProperty>> expression)
                {
                    MemberExpression memberExpression = expression.Body as MemberExpression;

                    if (memberExpression == null)
                    {
                        UnaryExpression unaryExpression = expression.Body as UnaryExpression;
                        if (unaryExpression != null)
                        {
                            memberExpression = (MemberExpression)unaryExpression.Operand;
                        }
                    }

                    if (memberExpression != null)
                    {
                        var propertyName = memberExpression.Member.Name;
                        PropertyInfo propertyInfo = typeof(TClass).GetProperty(propertyName);
                        var valorAntigo = propertyInfo.GetValue(ageSGIPMO, null);
                        var valorNovo = propertyInfo.GetValue(ageCDRE, null);

                        if (!valorAntigo.Equals(valorNovo))
                        {
                            propertyInfo.SetValue(ageSGIPMO, valorNovo, null);
                        }
                    }
                });
            }
        }

        public List<Agente> ObterAgentesPorIds(IList<int> idsAgente)
        {
            return agenteRepository.ObterAgentesPorIds(idsAgente);
        }

       
    }
}
