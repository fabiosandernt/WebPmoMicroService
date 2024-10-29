using ONS.Infra.Compression.Download;
using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Domain.Entities.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Interfaces
{

    public interface IArquivoService
    {


        byte[] ObterArquivoDadoNaoEstruturadoEmBytes(Guid idArquivo);

        /// <summary>
        /// Serviço utilizado para obter o download de um ou mais arquivos selecionados pelo usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>


        ResponseDownload ObterArquivosCompactados(RequestDownload request);

        /// <summary>
        /// Serviço utilizado para limpar os arquivos temporários que já foram gravados no banco de dados.
        /// </summary>
        /// <param name="arquivos"></param>


        void LimparArquivosTemporariosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos);



        Arquivo ObterArquivoDadoNaoEstruturado(Guid idArquivo);

        /// <summary>
        /// Salvando a coleta de dados não-estruturados. Os arquivos e a observação serão persistidos.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>



        void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto, DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInusmo = null);




        void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados);




        void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados);

        /// <summary>
        /// UC1004 - Convergir Informações CCEE 
        /// Método responsável por efetuar o início de Convergência com CCEE.
        /// </summary>
        /// <param name="dto"></param>



        void IniciarConvergenciaCCEE(InicializacaoConvergenciaCceeDTO dto);

        /// <summary>
        /// Método utilizado para efetuar a publicação de resultados de uma semana operativa.
        /// </summary>
        /// <param name="dto"></param>



        void PublicarResultados(PublicacaoResultadosDTO dto);

    }
}
