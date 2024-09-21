namespace ONS.WEBPMO.Servico.Usina
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    [ServiceKnownType(typeof(List<Subsistema>))]
    [ServiceKnownType(typeof(List<Reservatorio>))]
    [ServiceKnownType(typeof(List<Usina>))]
    [ServiceKnownType(typeof(List<UnidadeGeradora>))]
    [ServiceKnownType(typeof(List<Agente>))]
    [ServiceKnownType(typeof(List<Gabarito>))]
    [ServiceKnownType(typeof(List<ColetaInsumo>))]
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
        [OperationContract]
        IList<Reservatorio> ConsultarReservatorioPorNomeExibicao(string nome = "");

        /// <summary>
        /// Consulta reservatorios onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        [OperationContract]
        IList<Reservatorio> ConsultarReservatorioPorChaves(params string[] chaves);

        #endregion

        #region Usina

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeExibicaoContem, será aplicado o filtro de LIKE (%nomeExibicaoContem%)
        /// </summary>
        /// <param name="nomeExibicaoContem">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        IList<Usina> ConsultarUsinaPorNomeExibicao(string nome = "");

        /// <summary>
        /// Consulta usinas onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        IList<Usina> ConsultarUsinaPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta usinas sem parametro
        /// </summary>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        IList<Usina> ConsultarUsinas();

        #endregion


        #region Unidade Geradora

        /// <summary>
        /// Consulta unidades geradoras sem parametro
        /// </summary>
        /// <returns>Lista de Unidades Geradoras</returns>
        [OperationContract]
        IList<UnidadeGeradora> ConsultarUnidadesGeradoras();

        /// <summary>
        /// Consulta unidades geradoras onde as chaves sejam iguais as passadas por parametro
        /// </summary>
        /// <param name="chaves">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        [OperationContract]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorChaves(params string[] chaves);

        /// <summary>
        /// Consulta unidades geradoras de uma Usina especifica
        /// </summary>
        /// <param name="chaveUsina">Nome de exibição para filtrar</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        [OperationContract]
        IList<UnidadeGeradora> ConsultarUnidadeGeradoraPorUsina(string chave);

        #endregion

        #region Subsistemas

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        [OperationContract]
        IList<Subsistema> ConsultarTodosSubsistemas();

        /// <summary>
        /// Consulta todos os subsistemas na BDT
        /// </summary>
        /// <returns>Lista de subsistemas</returns>
        [OperationContract]
        IList<Subsistema> ConsultarSubsistemasAtivos();

        #endregion


        #region Agentes

        /// <summary>
        /// Consulta todos agentes que contém valor parcial (like) passado pelo parametro <paramref name="nomeParcial"/>
        /// </summary>
        /// <param name="nome">Valor parcial do nome do agente</param>
        /// <returns>Lista de Agentes</returns>
        [OperationContract]
        IList<Agente> ConsultarAgentesPorNome(string nome, int top = int.MaxValue);


        /// <summary>
        /// Consulta todos agentes com as chaves informadas
        /// </summary>
        /// <param name="chaves">Chaves dos Agentes</param>
        /// <returns>Lista de Agentes</returns>
        [OperationContract]
        IList<Agente> ConsultarAgentesPorChaves(params string[] chaves);

        #endregion

        #region Submercados

        /// <summary>
        /// Consulta todos os Submercados na BDT
        /// </summary>
        /// <returns>Lista de Submercados</returns>
        [OperationContract]
        IList<SubmercadoPMO> ConsultarSubmercados();

        /// <summary>
        /// Consulta Usinas PEM
        /// </summary>
        /// <returns>Lista de Usinas PEM</returns>
        [OperationContract]
        IList<UsinaPEM> ConsultarDadosUsinasVisaoPEM();

        #endregion

        #region ReservatorioEE
        /// <summary>
        /// Consulta dados de todos os REEs ativos
        /// </summary>
        /// <returns>Lista de todos os REEs ativos</returns>
        [OperationContract]
        IList<ReservatorioEE> ConsultarReservatoriosEquivalentesDeEnergiaAtivos();
        #endregion
    }
}
