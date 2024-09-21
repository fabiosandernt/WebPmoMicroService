
using ONS.WEBPMO.Domain.Repository.Base;

namespace ONS.WEBPMO.Domain.Repository
{

    public interface IInsumoRepository : IRepository<Insumo>
    {
        IList<InsumoEstruturado> ConsultarInsumoEstruturadoComGrandezaAtiva(TipoColetaEnum tipoColeta,
            CategoriaInsumoEnum? categoria = null);

        /// <summary>
        /// Consulta os insumos estruturados e não estruturados por parte do nome.
        /// Caso se trate de insumo estruturado, obter apenas os insumos com grandeza ativa.
        /// </summary>
        /// <param name="nomeInsumo">Parte do nome do insumo</param>
        /// <returns>Lista de insumos estruturados e não estruturados</returns>
        IList<Insumo> ConsultarPorNomeLike(string nomeInsumo);

        /// <summary>
        /// Consultar Insumo pelo nome (Restrito apenas ao nome)
        /// </summary>
        /// <param name="nomeInsumo"></param>
        /// <returns>Insumo</returns>
        Insumo ConsultarPorNome(string nomeInsumo);

        /// <summary>
        /// Consultar Insumo pela sigla (Restrito apenas a sigla)
        /// </summary>
        /// <param name="nomeInsumo"></param>
        /// <returns>Insumo</returns>
        Insumo ConsultarPorSigla(string siglaInsumo);

        /// <summary>
        /// Consultar insumo com grandeza ativa
        /// </summary>
        /// <returns>Lista de insumo estruturado e nao estruturado</returns>
        IList<Insumo> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtiva();

        /// <summary>
        /// Consultar insumo não estruturado
        /// </summary>
        /// <returns>Insumo não estruturado</returns>
        IList<InsumoNaoEstruturado> ConsultarInsumoNaoEstruturado();

        /// <summary>
        /// Consultar inusmo por ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Lista de insumos estruturados e não estruturados</returns>
        IList<Insumo> ConsultaInsumoPorIds(params int[] ids);

        /// <summary>
        /// Consultar insumo pelos campos preenchidos no filtro
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Lista de insumos estruturados e não estruturados</returns>
        IList<Insumo> ConsultarPorInsulmoFiltro(InsumoFiltro filtro);

        /// <summary>
        /// Obter insumos paginados
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Insumos</returns>
        PagedResult<Insumo> ConsultarPorInsumoFiltroPaginado(InsumoFiltro filtro);

        /// <summary>
        /// Obtém os insumos do estudo.
        /// </summary>
        /// <param name="idSemanaOperativa">Id do Estudo (semana operativa)</param>
        /// <param name="idsAgente">Ids dos Agentes do Estudo (semana operativa)</param>
        /// <returns>Lista de Insumos.</returns>
        IList<Insumo> ConsultarInsumosPorSemanaOperativaAgentes(int idSemanaOperativa, params int[] idsAgente);
    }
}
