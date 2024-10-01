using ONS.WEBPMO.Application.DTO;

namespace ONS.WEBPMO.Servico.Usina
{
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
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "");

        /// <summary>
        /// Consulta usinas Participantes
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Usinas Participantes</returns>
        
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
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "");

        /// <summary>
        /// Consulta Reservatórios Participantes do Gabarito
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Reservatórios Participantes do Gabarito</returns>
        
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
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "");

        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "");

        #endregion
    }
}
