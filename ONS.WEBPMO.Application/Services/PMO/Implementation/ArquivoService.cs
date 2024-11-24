using ONS.WEBPMO.Application.DTO;
using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class ArquivoService : IArquivoService
    {

        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IColetaInsumoService coletaInsumoService;
        private readonly IArquivoRepository arquivoRepository;

        public ArquivoService(IArquivoRepository arquivoRepository,
            IColetaInsumoService coletaInsumoService,
            ISemanaOperativaService semanaOperativaService)
        {
            this.arquivoRepository = arquivoRepository;
            this.coletaInsumoService = coletaInsumoService;
            this.semanaOperativaService = semanaOperativaService;
        }

        public void AprovarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            throw new NotImplementedException();
        }

        public void IniciarConvergenciaCCEE(InicializacaoConvergenciaCceeDTO dto)
        {
            throw new NotImplementedException();
        }

        public void LimparArquivosTemporariosUpload(ISet<ArquivoDadoNaoEstruturadoDTO> arquivos)
        {
            throw new NotImplementedException();
        }

        public Arquivo ObterArquivoDadoNaoEstruturado(Guid idArquivo)
        {
            throw new NotImplementedException();
        }

        public byte[] ObterArquivoDadoNaoEstruturadoEmBytes(Guid idArquivo)
        {
            throw new NotImplementedException();
        }

        //public ResponseDownload ObterArquivosCompactados(RequestDownload request)
        //{
        //    throw new NotImplementedException();
        //}

        public void PublicarResultados(PublicacaoResultadosDTO dto)
        {
            throw new NotImplementedException();
        }

        public void RejeitarColetaDadosNaoEstruturados(DadosMonitoramentoColetaInsumoDTO dtoColeta, DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dtoDados)
        {
            throw new NotImplementedException();
        }

        public void SalvarDadoColetaNaoEstruturada(DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO dto, DadosMonitoramentoColetaInsumoDTO dtoDadosAnalise, ColetaInsumo coletaInusmo = null)
        {
            throw new NotImplementedException();
        }
    }
}
