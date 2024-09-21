using ONS.Common.Services;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    using ONS.Common.Seguranca;
    using ONS.Common.Util.Pagination;
    using ONS.SGIPMO.Domain.Entities.Filters;

    [ServiceContract]
    public interface IGabaritoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        Gabarito ObterPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<GabaritoAgrupadoAgenteOrigemColetaDTO> ConsultarGabaritosAgrupadoPorAgenteTipoOrigemPaginado(GabaritoOrigemColetaFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void SalvarGabarito(GabaritoConfiguracaoDTO dto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AlterarGabarito(GabaritoConfiguracaoDTO dto);

        [OperationContract]
        [UseNetDataContractSerializer]
        Gabarito ObterPorColetaInsumoNaoEstruturado(GabaritoDadoColetaNaoEstruturadoFilter filtro);

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUsinaDTO>> ConsultarUsinaPorGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoUGEDTO>> ConsultarUGEPorGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoReservatorioDTO>> ConsultarReservatorioPorGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoSubsistemaDTO>> ConsultarSubsistemaPorGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoParticipantesBaseDTO<ReservatorioParticipanteGabaritoDTO>> ConsultarReservatoriosParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoParticipantesBaseDTO<UsinaParticipanteGabaritoDTO>> ConsultarUsinasParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoParticipantesBaseDTO<AgenteParticipanteGabaritoDTO>> ConsultarAgentesParticipantesGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoInsumoNaoEstruturadoDTO>> ConsultarAgentesPodemEnviarDadosNaoEstruturadosPorGabarito(bool isPadrao, string nomeRevisao = "");

        [OperationContract]
        [UseNetDataContractSerializer]
        IList<GabaritoConfiguracaoBaseDTO<ConfiguracaoGeracaoComplementar>> ConsultarAgentesComGeracaoComplementar(bool isPadrao, string nomeRevisao = "");


    }
}
