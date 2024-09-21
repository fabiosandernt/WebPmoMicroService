namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    [ServicePermission("ConfigurarInsumo")]
    public interface IInsumoService : IService
    {
        /// <summary>
        /// Consultar insumo com grandeza ativa
        /// </summary>
        /// <returns>Lista de insumo estruturado e nao estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Insumo> ConsultarInsumosNaoEstruturadoEEstruturadoComGrandezaAtiva();

        /// <summary>
        /// Consulta insumo pelo tipo origem coleta e categoria
        /// </summary>
        /// <param name="tipoOrigemColeta"></param>
        /// <param name="categoria"></param>
        /// <returns>Lista de insumo estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<InsumoEstruturado> ConsultarInsumoPorTipoOrigemColetaCategoria(TipoOrigemColetaEnum tipoOrigemColeta,
            CategoriaInsumoEnum? categoria = null);

        /// <summary>
        /// Consulta insumo estruturado pelo Id da usina
        /// </summary>
        /// <param name="idUsina"></param>
        /// <returns>Lista de insumo estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<InsumoEstruturado> ConsultarInsumoEstruturadosPorUsina(string idUsina);

        /// <summary>
        /// Consulta os insumos estruturados e não estruturados por parte do nome.
        /// Caso se trate de insumo estruturado, obter apenas os insumos com grandeza ativa.
        /// </summary>
        /// <param name="nomeInsumo">Parte do nome do insumo</param>
        /// <returns>Lista de insumos estruturados e não estruturados</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Insumo> ConsultarInsumosPorNome(string nomeInsumo);

        /// <summary>
        /// Consulta insumo não estruturado
        /// </summary>
        /// <returns>Lista de insumo não estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<InsumoNaoEstruturado> ConsultarInsumoNaoEstruturado();

        /// <summary>
        /// Consultar Todos os Insumos
        /// </summary>
        /// <returns>Lista de Insumos</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Insumo> ConsultarTodosInsumos();

        /// <summary>
        /// Consultar Insumos pelo Filtro de Pesquisa
        /// </summary>
        /// <returns>Lista de Insumos</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<Insumo> ConsultarInsumosPorFiltro(InsumoFiltro filtro);

        /// <summary>
        /// Inserir insumo não estruturado
        /// </summary>
        /// <param name="insumo"></param>
        /// <returns>Id do insumo não estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        int InserirInsumoNaoEstruturado(InsumoNaoEstruturado insumo);

        /// <summary>
        /// Obter Insumo não Estruturado pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        InsumoNaoEstruturado ObterInsumoNaoEstruturadoPorChave(int id);

        /// <summary>
        /// Adiciona insumo estruturado
        /// </summary>
        /// <param name="insumo"></param>
        /// <returns>Id do insumo estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        int InserirInsumoEstruturado(DadosInclusaoInsumoEstruturadoDTO insumo);

        /// <summary>
        /// Obtem um insumo estruturado por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Insumo estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer("CategoriaInsumo,TipoColeta,Grandezas.TipoDadoGrandeza.Id")]
        InsumoEstruturado ObterInsumoEstruturadoPorChave(int id);

        /// <summary>
        /// Alterar Insumo Nao Estruturado
        /// </summary>
        /// <param name="insumo"></param>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AlterarInsumoNaoEstruturado(InsumoNaoEstruturado insumo, byte[] versao);

        /// <summary>
        /// Alterar o Insumo Estruturado com as informações do DTO.
        /// </summary>
        /// <param name="dadosInsumoEstruturado">DTO com as informações do Insumo.</param>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AlterarInsumoEstruturado(DadosInclusaoInsumoEstruturadoDTO dadosInsumoEstruturado);

        /// <summary>
        /// Excluir Insumo por Id
        /// </summary>
        /// <param name="idInsumo"></param>
        /// <param name="versao"></param>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void ExcluirInsumoPorChave(int idInsumo, byte[] versao);

        /// <summary>
        /// Verificar se o insumo Estruturado está associado a algum gabarito
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True ou False</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        bool InsumoBloqueadosAlteracao(int id);

        /// <summary>
        ///  Verifica se o insumo é Reservado
        /// </summary>
        /// <param name="id"></param>
        [OperationContract]
        [UseNetDataContractSerializer]
        void VerificarInsumoReservado(int id);

        /// <summary>
        /// Obtem a grandeza por Id.
        /// </summary>
        /// <param name="idGrandeza">Id da grandeza.</param>
        /// <returns>Grandeza</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        Grandeza ObterGrandezaPorId(int idGrandeza);

        /// <summary>
        /// Valida a grandeza ao incluir/alterar.
        /// </summary>
        /// <param name="grandeza">Grandeza</param>
        [OperationContract]
        [UseNetDataContractSerializer]
        void ValidarIncluirAlterarGrandeza(Grandeza grandeza);

        /// <summary>
        /// Validar se é permito excluir a grandeza.
        /// Não permitindo excluir-la caso exista dado coleta associado.
        /// </summary>
        /// <param name="idGrandeza">Id da grandeza a ser validada</param>
        [OperationContract]
        [UseNetDataContractSerializer]
        void ValidarExclusaoGrandeza(int idGrandeza);

        /// <summary>
        /// Obtém os tipos de dados da grandeza.
        /// </summary>
        /// <returns>IList de TipoDadoGrandeza</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<TipoDadoGrandeza> ObterTiposDadoGrandeza();

        /// <summary>
        /// Obtém as grandezas de um determinado insumo.
        /// </summary>
        /// <param name="idInsumo">Id do Insumo</param>
        /// <returns>Lista de Grandezas.</returns>
        [OperationContract]
        [UseNetDataContractSerializer("TipoDadoGrandeza.Id")]
        IList<Grandeza> ObterGrandezasPorInsumo(int idInsumo);

        /// <summary>
        /// Obtém os insumos do estudo.
        /// </summary>
        /// <param name="idSemanaOperativa">Id do Estudo (semana operativa)</param>
        /// <param name="idsAgente">Ids dos Agentes do Estudo (semana operativa)</param>
        /// <returns>Lista de Insumos.</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<Insumo> ConsultarInsumosPorSemanaOperativaAgentes(int idSemanaOperativa, params int[] idsAgente);

        /// <summary>
        /// Verifica se a grandeza pode ser alterada.
        /// Só permite alterar grandeza que não teve dados coletados.
        /// </summary>
        /// <param name="idGrandeza">Id da grandeza</param>
        /// <returns>Lista de grandeza.</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        bool PermitirAlteracaoGrandeza(int idGrandeza);

        /// <summary>
        /// Adiciona insumo estruturado
        /// </summary>
        /// <param name="insumo"></param>
        /// <returns>Id do insumo estruturado</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        Insumo ConsultarInsumo(int id);
    }
}
