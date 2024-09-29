using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.Base;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IGabaritoService 
    {
        
        
        Gabarito ObterPorChave(int chave);

        
        
        PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO> ConsultarGabaritosAgrupadoPorAgenteTipoOrigemPaginado(GabaritoOrigemColetaFilter filter);

        
        
        
        void SalvarGabarito(GabaritoConfiguracaoDTO dto);

        
        
        
        void AlterarGabarito(GabaritoConfiguracaoDTO dto);

        
        
        Gabarito ObterPorColetaInsumoNaoEstruturado(GabaritoDadoColetaNaoEstruturadoFilter filtro);

        
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "");

        
        
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "");


    }
}
