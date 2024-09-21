using AutoMapper;

namespace ONS.WEBPMO.Servico.Usina
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GabaritoService : IGabaritoService
    {
        #region Usina

        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Usinas</returns>
        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarUsinaPorGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta usinas Participantes
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Usinas Participantes</returns>
        public IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarUsinasParticipantesGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>>>(lista);
            return result;
        }



        #endregion

        #region Unidade Geradora
        /// <summary>
        /// Consulta usinas
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Usinas</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Usinas</returns>
        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarUGEPorGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>>>(lista);
            return result;
        }

        #endregion

        #region Reservatorio
        /// <summary>
        /// Consulta Reservatorios
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Reservatorios</returns>
        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarReservatorioPorGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta Reservatorios Participantes
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Reservatorios</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Reservatorios Participantes</returns>
        public IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarReservatoriosParticipantesGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>>>(lista);
            return result;
        }


        #endregion


        #region Subsistemas

        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarSubsistemaPorGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>>>(lista);
            return result;
        }

        #endregion


        #region Agentes

        /// <summary>
        /// Consulta Agentes Participantes
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista de Agentes</returns>
        /// <param name="isPadrao">Filtra pelo gabarito padrão</param>
        /// <returns>Lista de Agentes Participantes</returns>
        public IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarAgentesParticipantesGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>>>(lista);
            return result;
        }

        #endregion

        #region Outros

        /// <summary>
        /// Consulta Agentes que podem Enviar dados não estruturados
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista Agentes que podem Enviar dados não estruturados</returns>
        /// <param name="isPadrao"></param>
        /// <returns>Lista Agentes que podem Enviar dados não estruturados</returns>
        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>>>(lista);
            return result;
        }

        /// <summary>
        /// Consulta Agentes com Geração complementar
        /// Caso seja informado o parâmetro nomeRevisao, será aplicado o filtro de LIKE (%nomeRevisao%)
        /// </summary>
        /// <param name="nomeRevisao">Nome da Revisao para filtrar</param>
        /// <returns>Lista Agentes com Geração complementar</returns>
        /// <param name="isPadrao"></param>
        /// <returns>Lista Agentes com Geração complementar</returns>
        public IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "")
        {
            var service = ApplicationContext.Resolve<ONS.SGIPMO.Domain.Services.IGabaritoService>();

            var lista = service.ConsultarAgentesComGeracaoComplementar(isPadrao, nomeRevisao);
            var result = Mapper.Map<IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>>>(lista);
            return result;
        }
        #endregion

    }

}
