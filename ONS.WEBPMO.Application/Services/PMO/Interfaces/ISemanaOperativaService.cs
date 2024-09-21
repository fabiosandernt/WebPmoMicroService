namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface ISemanaOperativaService : IService
    {
        /// <summary>
        /// Obtém a SemanaOperativa pela chave.
        /// </summary>
        /// <param name="chave">Chave da SemanaOperativa.</param>
        /// <returns>SemanaOperativa.</returns>
        [OperationContract]
        [UseNetDataContractSerializer("Situacao", "PMO")]
        SemanaOperativa ObterSemanaOperativaPorChave(int chave);

        /// <summary>
        /// Obtém a SemanaOperativa em situação válida para informar ValorDado do estudo.
        /// </summary>
        /// <param name="chave">Chave da SemanaOperativa.</param>
        /// <returns>SemanaOperativa.</returns>
        [OperationContract]
        [UseNetDataContractSerializer("Situacao")]
        SemanaOperativa ObterSemanaOperativaPorChaveParaInformarDados(int chave);

        /// <summary>
        /// Valida se a semana operativa selecionada está em situação possível de abertura de estudo
        /// e retorna a Semana caso válida.
        /// </summary>
        /// <param name="idSemanaOperativa">Id da semana operativa.</param>
        /// <returns>Retorna a SemanaOperativa caso esteja em situação válida.</returns>
        [OperationContract]
        [UseNetDataContractSerializer("PMO")]
        SemanaOperativa ObterSemanaOperativaValidaParaAbrirEstudo(DadosSemanaOperativaDTO dto);

        /// <summary>
        /// Consulta as semanas operativas que já foram associada a um gabarito.
        /// </summary>
        /// <returns>Lista de SemanaOperativa.</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<SemanaOperativa> ConsultarSemanasOperativasComGabarito();

        /// <summary>
        /// Consulta as semanas operativas com estudo aberto por nome.
        /// </summary>
        /// <returns>Lista de SemanaOperativa.</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<SemanaOperativa> ConsultarEstudoPorNome(string nomeEstudo);

        /// <summary>
        /// Serviço utilizado para retornar os estudos passíveis de convergência de PLD.
        /// </summary>
        /// <param name="nomeEstudo"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        IList<SemanaOperativa> ConsultarEstudoConvergenciaPldPorNome(string nomeEstudo);

        /// <summary>
        /// Gerar as semanas operativas para o PMO.
        /// </summary>
        /// <param name="ano">Ano do PMO</param>
        /// <param name="mes">Mês do PMO</param>
        /// <returns>Lista de SemanaOperativa</returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        ISet<SemanaOperativa> GerarSugestaoSemanasOperativas(int ano, int mes);

        /// <summary>
        /// Abre estudo para a semana selecionada.
        /// </summary>
        /// <param name="dto">Propriedades do DTO: 
        /// "IdSemanaOperativa": Id da semana operativa.
        /// "IdSemanaEstudoGabarito": Id da semana onde vai ser "copiado" o gabarito.
        /// "IsPadrao": Indica se o gabarito deve ser resetado para o grabarito padrão.
        /// "VersaoPMO": Identificação da versão do PMO a fim de tratar a concorrência de registros
        /// "VersaoSemanaOperativa": Identificação da versão da Semana Operativa a fim de tratar a concorrência de registros
        /// </param>
        [OperationContract]
        [TransactionRequired]
        void AbrirEstudo(AberturaEstudoDTO dto);

        /// <summary>
        /// Gera a SemanaOperativa para o PMO.
        /// </summary>
        /// <param name="ano">Ano do PMO.</param>
        /// <param name="nomeMes">Mês PMO.</param>
        /// <param name="dataInicioSemana">Data de início da SemanaOperativa.</param>
        /// <param name="dataFimPMO">Data de fim da SemanaOperativa.</param>
        /// <param name="revisao">Número da revisão.</param>
        /// <returns>SemanaOperativa</returns>
        SemanaOperativa GerarSemanaOperativa(int ano, string nomeMes, DateTime dataInicioSemana,
            DateTime dataFimPMO, int revisao);

        /// <summary>
        /// Atualiza as SemanasOperativas após uma inclusão de uma nova SemanaOperativa.
        /// </summary>
        /// <param name="semanasOperativas">Lista de SemanaOperativa do PMO.</param>
        /// <param name="ano">Ano do PMO.</param>
        /// <param name="nomeMes">Mês do PMO.</param>
        void AtualizarSemanasOperativasInclusao(IEnumerable<SemanaOperativa> semanasOperativas,
            int ano, string nomeMes);

        /// <summary>
        /// Exclui a semana operativa
        /// </summary>
        /// <param name="semanaOperativa">SemanaOperativa</param>
        void ExcluirSemana(SemanaOperativa semanaOperativa);

        /// <summary>
        /// Altera a semana operativa.
        /// </summary>
        /// <param name="dadosAlteracao">ValorDado para alteração.</param>
        [OperationContract]
        [TransactionRequired]
        void AlterarSemanaOperativa(DadosAlteracaoSemanaOperativaDTO dadosAlteracao);

        /// <summary>
        /// Reseta o gabarito de uma semana operativa para o gabarito selecionado.
        /// </summary>
        /// <param name="dto">Propriedades do DTO: 
        /// "IdSemanaOperativa": Id da semana operativa.
        /// "IdSemanaEstudoGabarito": Id da semana onde vai ser "copiado" o gabarito.
        /// "IsPadrao": Indica se o gabarito deve ser resetado para o grabarito padrão.
        /// "VersaoPMO": Identificação da versão do PMO a fim de tratar a concorrência de registros
        /// </param>
        [OperationContract]
        [TransactionRequired]
        void ResetarGabarito(ResetGabaritoDTO dto);

        /// <summary>
        /// Valida se a SemanaOperativa foi selecionada e retorna a SemanaOperativa.
        /// </summary>
        /// <param name="idSemanaOperativa">Id da semana operativa.</param>
        /// <returns>Retorna a SemanaOperativa caso esteja em situação válida.</returns>
        [OperationContract]
        [UseNetDataContractSerializer("PMO", "Situacao")]
        SemanaOperativa ObterSemanaOperativaValidaParaResetarGabarito(int idSemanaOperativa);

        #region Convergência CCEE

        /// <summary>
        /// UC1004 - Convergir Informações CCEE
        /// Este método tem o objetivo de consultar arquivos de uma determinada semana operativa que serão visualizados no momento de efetuar a Convergência CCEE.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer("ArquivosInsumos", "ArquivosEnviados", "SituacaoSemanaOperativa")]
        ArquivosSemanaOperativaDTO ConsultarArquivosSemanaOperativaConvergenciaCcee(ArquivosSemanaOperativaFilter filtro);

        /// <summary>
        /// UC1004 - Convergir Informações CCEE 
        /// Método responsável por efetuar o início de Convergência com CCEE.
        /// </summary>
        /// <param name="dto"></param>
        void IniciarConvergenciaCCEE(InicializacaoConvergenciaCceeDTO dto);

        #endregion

        #region Convergir PLD

        /// <summary>
        /// Serviço que consulta os arquivos de insumos coletados durante o processo de coleta e também os arquivos que foram enviados ao iniciar Convergência com CCEE.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer("Arquivos", "SemanaOperativa", "SemanaOperativa.Situacao")]
        ArquivosSemanaOperativaConvergirPldDTO ConsultarArquivosSemanaOperativaConvergenciaPLD(ArquivosSemanaOperativaFilter filtro);

        /// <summary>
        /// Método utilizado para realizar a convergência com PLD ("Convergir PLD" ou "Não Convergir PLD")
        /// </summary>
        /// <param name="dto"></param>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void ConvergirPLD(ConvergirPLDDTO dto);


        #endregion

        #region Publicação de Resultados

        /// <summary>
        /// Método utilizado para consultar os arquivos a serem considerados na etapa de publicação de resultados.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer("ArquivosInsumos", "ArquivosEnviados", "SituacaoSemanaOperativa")]
        ArquivosSemanaOperativaDTO ConsultarArquivosSemanaOperativaPublicacaoResultados(ArquivosSemanaOperativaFilter filtro);

        /// <summary>
        /// Método utilizado para efetuar a publicação de resultados de uma semana operativa.
        /// </summary>
        /// <param name="dto"></param>
        void PublicarResultados(PublicacaoResultadosDTO dto);

        /// <summary>
        /// Método utilizado para efetuar o reprocessamento do PMO, que é acionado na mesma tela em que os resultados são publicados.
        /// </summary>
        /// <param name="dto"></param>
        [OperationContract]
        [TransactionRequired]
        [UseNetDataContractSerializer]
        void ReprocessarPMO(ReprocessamentoPMODTO dto);

        #endregion

    }
}
