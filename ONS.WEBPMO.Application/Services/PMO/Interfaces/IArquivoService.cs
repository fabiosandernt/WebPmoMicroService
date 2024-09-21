using System;
using System.Collections.Generic;
using ONS.Common.Services;
using ONS.Common.Util.Files;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System.ServiceModel;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IArquivoService : IService
    {

        [OperationContract]
        byte[] ObterArquivoDadoNaoEstruturadoEmBytes(Guid idArquivo);

        /// <summary>
        /// Serviço utilizado para obter o download de um ou mais arquivos selecionados pelo usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        ResponseDownload ObterArquivosCompactados(RequestDownload request);

        /// <summary>
        /// Serviço utilizado para limpar os arquivos temporários que já foram gravados no banco de dados.
        /// </summary>
        /// <param name="arquivos"></param>
        [OperationContract]
        [UseNetDataContractSerializer]
        void LimparArquivosTemporariosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos);

        [OperationContract]
        [UseNetDataContractSerializer]
        Arquivo ObterArquivoDadoNaoEstruturado(Guid idArquivo);

        /// <summary>
        /// Salvando a coleta de dados não-estruturados. Os arquivos e a observação serão persistidos.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto, DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInusmo = null);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados);

        /// <summary>
        /// UC1004 - Convergir Informações CCEE 
        /// Método responsável por efetuar o início de Convergência com CCEE.
        /// </summary>
        /// <param name="dto"></param>
        [OperationContract]
        [TransactionRequired]
        [UseNetDataContractSerializer]
        void IniciarConvergenciaCCEE(InicializacaoConvergenciaCceeDTO dto);

        /// <summary>
        /// Método utilizado para efetuar a publicação de resultados de uma semana operativa.
        /// </summary>
        /// <param name="dto"></param>
        [OperationContract]
        [TransactionRequired]
        [UseNetDataContractSerializer]
        void PublicarResultados(PublicacaoResultadosDTO dto);

    }
}
