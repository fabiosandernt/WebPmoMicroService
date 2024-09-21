using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface IUnidadeGeradoraPMORepository : IRepository<UnidadeGeradoraPMO>
    {
        /// <summary>
        /// Consulta unidades geradoras do PMO sem parametro
        /// </summary>
        /// <returns>Lista de Unidades Geradoras do PMO</returns>
        IList<UnidadeGeradoraPMO> Consultar();

        /// <summary>
        /// Consulta unidades geradoras do PMO onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras do PMO</returns>
        IList<UnidadeGeradoraPMO> ConsultarPorChaves(string[] chaves);

        /// <summary>
        /// Consulta unidades geradoras do PMO de uma Usina especifica
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras do PMO</returns>
        IList<UnidadeGeradoraPMO> ConsultarPorUsina(string chaveUsina);
    }
}
