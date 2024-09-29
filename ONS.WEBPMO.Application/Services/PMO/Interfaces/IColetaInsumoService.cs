using ONS.Infra.Core.Pagination;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.Filters;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{
    //[ServiceContract]
    public interface IColetaInsumoService 
    {
        
        
        ColetaInsumo ObterPorChave(int chave);

        
        //[UseNetDataContractSerializer("Situacao", "Insumo", "Insumo.TipoColeta", "Agente", "SemanaOperativa", "SemanaOperativa.Situacao")]
        
        ColetaInsumo ObterValidarColetaInsumoInformarDadosPorChave(int idColetaInsumo, byte[] versaoColetaInsumo = null, bool atualizaParaAndamento = false);

        
        //[UseNetDataContractSerializer("Situacao", "Insumo", "Insumo.TipoColeta", "Agente", "SemanaOperativa", "SemanaOperativa.Situacao")]
        ColetaInsumo ObterColetaInsumoInformarDadosPorChave(int idColetaInsumo);

        
        //[UseNetDataContractSerializer("Situacao", "Insumo", "Insumo.TipoColeta", "Agente", "SemanaOperativa", "SemanaOperativa.Situacao")]
        ColetaInsumo ObterValidarColetaInsumoMonitorarDadosPorChave(int idColetaInsumo, int idSituacaoColeta);

        
        
        DadosInformarColetaInsumoDTO ObterDadosParaInformarDadosPorChaveColetaInsumo(ColetaInsumoFilter filter);

        
        //[UseNetDataContractSerializer("Situacao", "Insumo", "Agente")]
        PagedResult<ColetaInsumo> ConsultarColetasInsumoParaInformarDadosPaginado(PesquisaMonitorarColetaInsumoFilter filter);

        
        //[UseNetDataContractSerializer("Situacao", "Insumo", "Agente")]
        PagedResult<ColetaInsumo> ConsultarColetasInsumoParaMonitorarDadosPaginado(
            PesquisaMonitorarColetaInsumoFilter filter);

        
        
        
        void EnviarDadosColetaInsumo(EnviarDadosColetaInsumoFilter filter);

        
        
        
        void EnviarDadosColetaInsumoManutencao(EnviarDadosColetaInsumoManutencaoFilter filter);

        
        
        PagedResult<DadoColetaManutencaoDTO> ConsultarDadoColetaManutencaoPorColetaInsumoPaginado(DadoColetaInsumoFilter filter);

        
        
        
        void IncluirDadoColetaManutencao(InclusaoDadoColetaManutencaoDTO dto);

        
        
        
        void ExcluirDadoColetaManutencao(ExclusaoDadoColetaManutencaoDTO dto, int idColetaInsumo);

        
        
        
        void AlterarDadoColetaManutencao(AlteracaoDadoColetaManutencaoDTO dto);

        
        
        
        void IncluirDadoColetaManutencaoImportacao(IList<InclusaoDadoColetaManutencaoDTO> dtos);

        
        
        
        void SalvarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, DadoColetaInsumoDTO dto);

        
        
        
        void EnviarColetaDadosEstruturados(IList<ValorDadoColetaDTO> dtos, int idColetaInsumo, string versao);

        
        
        
        void SalvarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter);

        
        
        
        void AprovarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter);

        
        
        
        void RejeitarColetaDadosManutencao(ColetaInsumoManutencaoFilter filter);

        
        
        
        void CapturarColetaDados(DadosMonitoramentoColetaInsumoDTO dto);

        
        
        
        bool situacaoBoolSemanaOperativa(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        
        
        
        void AbrirColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        
        
        
        void FecharColeta(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        
        
        
        void AprovarColetaDadosEstruturados(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto);

        
        
        
        void AprovarColetaDadosEstruturadosEmLote(DadoColetaInsumoDTO dadoColetaInsumoDto, IList<ValorDadoColetaDTO> valoresDto);

        
        
        
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
        
        //[UseNetDataContractSerializer("Arquivos")]
        DadoColetaNaoEstruturadoDTO ObterDadoColetaNaoEstruturado(DadoColetaInsumoNaoEstruturadoFilter filtro);

        ISet<Arquivo> ObterArquivosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos, bool desconsiderarJaGravadosBancoDados = false);

        #endregion

        IList<DadoColetaBloco> ConsultarDadosColetaParaGeracaoBloco(int idSemanaOperativa);

        
        
        
        void DeletarArquivos(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        
        
        
        Parametro MensagemAberturaColetaEditavel(DadosSemanaOperativaDTO dadosSemanaOperativaDto);

        
        
        
        bool VerificarPermissaoIncluirManutencao();

        
        
        
        bool VerificarSeDadosInsumoIguaisColetaAnterior(ColetaInsumo coletaInsumo);

        
        string ChecarSeVolumeInicialIgualAoDaSemanaAnterior(IList<ValorDadoColetaDTO> valorDadoColetaDTOs, int idColetaInsumo);
    }
}
