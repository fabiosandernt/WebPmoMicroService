using ONS.WEBPMO.Domain.Entities.BDT;
using ONS.WEBPMO.Domain.Entities.Usina;
using ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina;

namespace ONS.WEBPMO.Servico.Usina
{

    public interface IBDTService
    {

        #region Reservatorio

        /// <summary>
        /// Consulta de reservatorios
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%);
        /// Caso seja informado o parâmetro quantidadeMaxima, será retornado apenas a quantidade de registros informado;
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>

        IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nome = "");

        /// <summary>
        /// Consulta reservatorios onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>

        IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves);

        #endregion

        #region Usina

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>

        IList<ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina.Usina> ConsultarUsinaPorNomeExibicao(string nome = "");

        /// <summary>
        /// Consulta usinas onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>

        IList<ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina.Usina> ConsultarUsinaPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta usinas sem parametro
        /// </summary>
        /// <returns>Lista de Usinas</returns>

        IList<ONS.WEBPMO.Domain.Entities.Usina.OrigemColetaUsina.Usina> ConsultarUsinas();

        #endregion


        #region Unidade Geradora

        /// <summary>
        /// Consulta unidades geradoras sem parametro
        /// </summary>
        /// <returns>Lista de Unidades Geradoras</returns>

        IList<UnidadeGeradora> ConsultarUnidadesGeradoras();

        /// <summary>
        /// Consulta unidades geradoras onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>

        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta unidades geradoras de uma Usina especifica
        /// </summary>
        /// <param name="chaveUsina">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>

        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chave);

        #endregion

        #region Subsistemas

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>

        IList<Subsistema> ConsultarTodosSubsistemas();

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>

        IList<Subsistema> ConsultarSubsistemasAtivos();

        #endregion


        #region Agentes

        /// <summary>
        /// Consulta todos agentes que contém valor parcial (like) passado pelo parametro <paramref name="nomeParcial"/>
        /// </summary>
        /// <param name="nome">Valor parcial do nome do agente</param>
        /// <returns>Lista de Agentes</returns>

        IList<Agente> ConsultarAgentesPorNome(string nome, int top = int.MaxValue);


        /// <summary>
        /// Consulta todos agentes com as chaves informadas
        /// </summary>
        /// <param name="chaves">Chaves dos Agentes</param>
        /// <returns>Lista de Agentes</returns>

        IList<Agente> ConsultarAgentesPorChaves(params string[] chaves);

        #endregion

        #region Submercados

        /// <summary>
        /// Consulta todos os Submercados na BDT
        /// </summary>
        /// <returns>Lista de Submercados</returns>

        IList<SubmercadoPMO> ConsultarSubmercados();

        /// <summary>
        /// Consulta Usinas PEM
        /// </summary>
        /// <returns>Lista de Usinas PEM</returns>

        IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM();

        #endregion

        #region ReservatorioEE
        /// <summary>
        /// Consulta dados de todos os REEs ativos
        /// </summary>
        /// <returns>Lista de todos os REEs ativos</returns>

        IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos();
        #endregion
    }
}
