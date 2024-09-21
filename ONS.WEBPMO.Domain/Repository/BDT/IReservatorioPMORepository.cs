using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository.BDT
{
    public interface IReservatorioPMORepository : IRepository<ReservatorioPMO>
    {
        /// <summary>
        /// Consulta reservatorios do PMO
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios do PMO</returns>
        IList<ReservatorioPMO> ConsultarPorNomeExibicao(string nomeExibicaoContem);

        /// <summary>
        /// Consulta reservatorios do PMO onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios do PMO</returns>
        IList<ReservatorioPMO> ConsultarPorChaves(string[] chaves);
    }
}
