using System.Collections.Generic;
using ONS.Common.Services;
using ONS.Common.Util.Files;
using ONS.Common.Util.Pagination;
using ONS.Common.Wcf;
using ONS.SGIPMO.Domain.Entities;
using System.ServiceModel;
using ONS.SGIPMO.Domain.Entities.DTO;
using ONS.SGIPMO.Domain.Entities.Filters;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    [ServiceContract]
    public interface IColetaInsumoService : IService
    {
        [OperationContract]
        [UseNetDataContractSerializer]
        ColetaInsumo ObterPorChave(int chave);

        [OperationContract]
        [UseNetDataContractSerializer("Situacao", "Insumo", "Insumo.TipoColeta", "Agente", "SemanaOperativa", "SemanaOperativa.Situacao")]
        [TransactionRequired]
        ColetaInsumo ObterValidarColetaInsumoInformarDadosPorChave(int idColetaInsumo, byte[] versaoColetaInsumo = null, bool atualizaParaAndamento = false);

        [OperationContract]
        [UseNetDataContractSerializer("Situacao", "Insumo", "Insumo.TipoColeta", "Agente", "SemanaOperativa", "SemanaOperativa.Situacao")]
        ColetaInsumo ObterColetaInsumoInformarDadosPorChave(int idColetaInsumo);

        [OperationContract]
        [UseNetDataContractSerializer("Situacao", "Insumo", "Insumo.TipoColeta", "Agente", "SemanaOperativa", "SemanaOperativa.Situacao")]
        ColetaInsumo ObterValidarColetaInsumoMonitorarDadosPorChave(int idColetaInsumo, int idSituacaoColeta);

        [OperationContract]
        [UseNetDataContractSerializer]
        DadosInformarColetaInsumoDTO ObterDadosParaInformarDadosPorChaveColetaInsumo(ColetaInsumoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer("Situacao", "Insumo", "Agente")]
        PagedResult<ColetaInsumo> ConsultarColetasInsumoParaInformarDadosPaginado(PesquisaMonitorarColetaInsumoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer("Situacao", "Insumo", "Agente")]
        PagedResult<ColetaInsumo> ConsultarColetasInsumoParaMonitorarDadosPaginado(
            PesquisaMonitorarColetaInsumoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void EnviarDadosColetaInsumo(EnviarDadosColetaInsumoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void EnviarDadosColetaInsumoManutencao(EnviarDadosColetaInsumoManutencaoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(DadoColetaInsumoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void IncluirDadoColetaManutencao(InclusaoDadoColetaManutencaoDTO dto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void ExcluirDadoColetaManutencao(ExclusaoDadoColetaManutencaoDTO dto, int idColetaInsumo);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AlterarDadoColetaManutencao(AlteracaoDadoColetaManutencaoDTO dto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void IncluirDadoColetaManutencaoImportacao(IList<InclusaoDadoColetaManutencaoDTO> dtos);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void SalvarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, DadoColetaInsumoDTO dto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void EnviarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, int idColetaInsumo, string versao);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void SalvarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AprovarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void RejeitarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void CapturarColetaDados(DadosMonitoramentoColetaInsumoDTO dto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        bool situacaoBoolSemanaOperativa(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AbrirColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void FecharColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AprovarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void AprovarColetaDadosEstruturadosEmLote(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void RejeitarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto);

        #region Coleta e Monitoramento de Insumo Não-Estruturado

        void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados);

        void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados);

        void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto, DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInusmo = null);

        /// <summary>
        /// Método utilizado para consultar as informações de um DadoColeta que seja do tipo não estruturado. Serão considerados os arquivos no retorno do mesmo.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [OperationContract]
        [UseNetDataContractSerializer("Arquivos")]
        DadoColetaNaoEstruturadoDTO ObterDadoColetaNaoEstruturado(DadoColetaInsumoNaoEstruturadoFilter filtro);

        ISet<Arquivo> ObterArquivosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, bool desconsiderarJaGravadosBancoDados = false);

        #endregion

        IList<DadoColetaBloco> ConsultarDadosColetaParaGeracaoBloco(int idSemanaOperativa);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        void DeletarArquivos(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        Parametro MensagemAberturaColetaEditavel(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        bool VerificarPermissaoIncluirManutencao();

        [OperationContract]
        [UseNetDataContractSerializer]
        [TransactionRequired]
        bool VerificarSeDadosInsumoIguaisColetaAnterior(ColetaInsumo coletaInsumo);

        [OperationContract]
        string ChecarSeVolumeInicialIgualAoDaSemanaAnterior(IList<ValorDadoColetaDTO> valorDadoColetaDTOs, int idColetaInsumo);
    }
}
