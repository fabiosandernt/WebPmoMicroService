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
    [ServiceKnownType(typeof(List<string>))]
    public interface IGabaritoService
    {
        #region Agentes

        /// <summary>
        /// Consulta Agentes
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Agentes</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Agentes</returns>
        [OperationContract]
        IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        #endregion

        #region Usina

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Usinas</returns>
        [OperationContract]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "");

        /// <summary>
        /// Consulta usinas Participantes
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Usinas Participantes</returns>
        [OperationContract]
        IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "");
        #endregion

        #region Unidade Geradora
        /// <summary>
        /// Consulta Geradoras
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Unidades Geradoras</returns>
        [OperationContract]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "");

        #endregion

        #region Reservatorio
        /// <summary>
        /// Consulta Reservatórios
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Reservatórios</returns>
        [OperationContract]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "");

        /// <summary>
        /// Consulta Reservatórios Participantes do Gabarito
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Reservatórios Participantes do Gabarito</returns>
        [OperationContract]
        IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "");


        #endregion

        #region Subsistemas

        /// <summary>
        /// Consulta Reservatórios
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Reservatórios</returns>
        [OperationContract]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "");

        #endregion

        #region Outros

        /// <summary>
        /// Consulta Agentes que podem Enviar dados não estruturados
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista Agentes que podem Enviar dados não estruturados</returns>
        /// <param name="isPadrao">Lista Agentes que podem Enviar dados não estruturados</param>
        /// <returns>Lista de Reservatórios</returns>
        [OperationContract]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "");

        #endregion
    }
}
