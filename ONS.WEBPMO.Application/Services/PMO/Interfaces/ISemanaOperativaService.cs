using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    public interface ISemanaOperativaService
    {
        /// <summary>
        /// Obtém a SemanaOperativa pela chave.
        /// </summary>
        /// <param name="chave">Chave da SemanaOperativa.</param>
        /// <returns>SemanaOperativa.</returns>
        ValueTask<SemanaOperativa> ObterSemanaOperativaPorChaveAsync(int chave);

        /// <summary>
        /// Obtém a SemanaOperativa em situação válida para informar ValorDado do estudo.
        /// </summary>
        /// <param name="chave">Chave da SemanaOperativa.</param>
        /// <returns>SemanaOperativa.</returns>
        ValueTask<SemanaOperativa> ObterSemanaOperativaPorChaveParaInformarDadosAsync(int chave);

        /// <summary>
        /// Valida se a semana operativa selecionada está em situação possível de abertura de estudo
        /// e retorna a Semana caso válida.
        /// </summary>
        /// <param name="dto">Dados da semana operativa.</param>
        /// <returns>Retorna a SemanaOperativa caso válida.</returns>
        Task<SemanaOperativa> ObterSemanaOperativaValidaParaAbrirEstudoAsync(DadosSemanaOperativaDTO dto);

        /// <summary>
        /// Consulta as semanas operativas que já foram associadas a um gabarito.
        /// </summary>
        /// <returns>Lista de SemanaOperativa.</returns>
        Task<IList<SemanaOperativa>> ConsultarSemanasOperativasComGabaritoAsync();

        /// <summary>
        /// Consulta as semanas operativas com estudo aberto por nome.
        /// </summary>
        /// <returns>Lista de SemanaOperativa.</returns>
        Task<IList<SemanaOperativa>> ConsultarEstudoPorNomeAsync(string nomeEstudo);

        /// <summary>
        /// Serviço utilizado para retornar os estudos passíveis de convergência de PLD.
        /// </summary>
        /// <param name="nomeEstudo">Nome do estudo.</param>
        /// <returns>Lista de SemanaOperativa.</returns>
        Task<IList<SemanaOperativa>> ConsultarEstudoConvergenciaPldPorNomeAsync(string nomeEstudo);

        /// <summary>
        /// Gera as semanas operativas para o PMO.
        /// </summary>
        /// <param name="ano">Ano do PMO.</param>
        /// <param name="mes">Mês do PMO.</param>
        /// <returns>Lista de SemanaOperativa.</returns>
        ISet<SemanaOperativa> GerarSugestaoSemanasOperativas(int ano, int mes);

        /// <summary>
        /// Abre estudo para a semana selecionada.
        /// </summary>
        /// <param name="dto">Propriedades do DTO.</param>
        Task AbrirEstudoAsync(AberturaEstudoDTO dto);

        /// <summary>
        /// Gera a SemanaOperativa para o PMO.
        /// </summary>
        /// <param name="ano">Ano do PMO.</param>
        /// <param name="nomeMes">Mês PMO.</param>
        /// <param name="dataInicioSemana">Data de início da SemanaOperativa.</param>
        /// <param name="dataFimPMO">Data de fim da SemanaOperativa.</param>
        /// <param name="revisao">Número da revisão.</param>
        /// <returns>SemanaOperativa.</returns>
        SemanaOperativa GerarSemanaOperativa(int ano, string nomeMes, DateTime dataInicioSemana, DateTime dataFimPMO, int revisao);

        /// <summary>
        /// Atualiza as SemanasOperativas após a inclusão de uma nova SemanaOperativa.
        /// </summary>
        /// <param name="semanasOperativas">Lista de SemanaOperativa do PMO.</param>
        /// <param name="ano">Ano do PMO.</param>
        /// <param name="nomeMes">Mês do PMO.</param>
        Task AtualizarSemanasOperativasInclusaoAsync(IEnumerable<SemanaOperativa> semanasOperativas,
            int ano, string nomeMes);

        /// <summary>
        /// Exclui a semana operativa.
        /// </summary>
        /// <param name="semanaOperativa">SemanaOperativa.</param>
        Task ExcluirSemanaAsync(SemanaOperativa semanaOperativa);

        /// <summary>
        /// Altera a semana operativa.
        /// </summary>
        /// <param name="dadosAlteracao">Valor para alteração.</param>
        Task AlterarSemanaOperativaAsync(DadosAlteracaoSemanaOperativaDTO dadosAlteracao);

        /// <summary>
        /// Reseta o gabarito de uma semana operativa para o gabarito selecionado.
        /// </summary>
        /// <param name="dto">Propriedades do DTO.</param>
        Task ResetarGabaritoAsync(ResetGabaritoDTO dto);

        /// <summary>
        /// Valida se a SemanaOperativa foi selecionada e retorna a SemanaOperativa.
        /// </summary>
        /// <param name="idSemanaOperativa">Id da semana operativa.</param>
        /// <returns>Retorna a SemanaOperativa caso esteja em situação válida.</returns>
        ValueTask<SemanaOperativa> ObterSemanaOperativaValidaParaResetarGabaritoAsync(int idSemanaOperativa);

        #region Convergência CCEE

        /// <summary>
        /// UC1004 - Convergir Informações CCEE.
        /// Consulta arquivos de uma determinada semana operativa para efetuar a Convergência CCEE.
        /// </summary>
        /// <param name="filtro">Filtro para consulta.</param>
        /// <returns>Arquivos da semana operativa.</returns>
        Task<ArquivosSemanaOperativaDTO> ConsultarArquivosSemanaOperativaConvergenciaCceeAsync(ArquivosSemanaOperativaFilter filtro);

        /// <summary>
        /// UC1004 - Convergir Informações CCEE.
        /// Inicia a Convergência com CCEE.
        /// </summary>
        /// <param name="dto">Dados de inicialização.</param>
        Task IniciarConvergenciaCCEEAsync(InicializacaoConvergenciaCceeDTO dto);

        #endregion

        #region Convergir PLD

        /// <summary>
        /// Consulta os arquivos de insumos coletados durante o processo de coleta e também os arquivos enviados ao iniciar Convergência com CCEE.
        /// </summary>
        /// <param name="filtro">Filtro para a consulta.</param>
        /// <returns>Arquivos da semana operativa para convergência de PLD.</returns>
        Task<ArquivosSemanaOperativaConvergirPldDTO> ConsultarArquivosSemanaOperativaConvergenciaPLDAsync(ArquivosSemanaOperativaFilter filtro);

        /// <summary>
        /// Realiza a convergência com PLD.
        /// </summary>
        /// <param name="dto">Dados da convergência.</param>
        Task ConvergirPLDAsync(ConvergirPLDDTO dto);

        #endregion

        #region Publicação de Resultados

        /// <summary>
        /// Consulta os arquivos a serem considerados na etapa de publicação de resultados.
        /// </summary>
        /// <param name="filtro">Filtro para a consulta.</param>
        /// <returns>Arquivos da semana operativa para a publicação de resultados.</returns>
        Task<ArquivosSemanaOperativaDTO> ConsultarArquivosSemanaOperativaPublicacaoResultadosAsync(ArquivosSemanaOperativaFilter filtro);

        /// <summary>
        /// Efetua a publicação de resultados de uma semana operativa.
        /// </summary>
        /// <param name="dto">Dados da publicação.</param>
        Task PublicarResultadosAsync(PublicacaoResultadosDTO dto);

        /// <summary>
        /// Efetua o reprocessamento do PMO na etapa de publicação de resultados.
        /// </summary>
        /// <param name="dto">Dados do reprocessamento.</param>
        Task ReprocessarPMOAsync(ReprocessamentoPMODTO dto);

        #endregion
    }
}
