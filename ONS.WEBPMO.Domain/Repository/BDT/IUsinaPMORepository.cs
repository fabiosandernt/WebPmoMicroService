
using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface IUsinaPMORepository : IRepository<UsinaPMO>
    {
        /// <summary>
        /// Consulta usinas do PMO
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas do PMO</returns>
        IList<UsinaPMO> ConsultarPorNomeExibicao(string nomeExibicaoContem);

        /// <summary>
        /// Consulta usinas do PMO onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas do PMO</returns>
        IList<UsinaPMO> ConsultarPorChaves(string[] chaves);

        /// <summary>
        /// Consulta Usina por CodUsinaPlanejamento e tipo de Geração
        /// </summary>
        /// <param name="CodUsinaPlanejamento"></param>
        /// <param name="codTipoGeracao"></param>
        /// <returns></returns>
        UsinaPMO Consultar(int CodUsinaPlanejamento, string codTipoGeracao);

        /// <summary>
        /// Consulta usinas do PMO
        /// </summary>
        /// <returns>Lista de Usinas do PMO</returns>
        IList<UsinaPMO> Consultar();
    }

}
