using ONS.WEBPMO.Application.Services.PMO.Interfaces;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class DadoColetaNaoEstruturadoService : Service, IDadoColetaNaoEstruturadoService
    {
        private IDadoColetaNaoEstruturadoRepository DadoColetaNaoEstruturadoRepository { get; set; }
        private ISemanaOperativaRepository SemanaOperativaRepository { get; set; }
        private IColetaInsumoRepository ColetaInsumoRepository { get; set; }
        private IGabaritoRepository GabaritoRepository { get; set; }
        private ISituacaoColetaInsumoRepository SituacaoColetaInsumoRepository { get; set; }
        private IArquivoRepository ArquivoRepository { get; set; }

        public DadoColetaNaoEstruturadoService(
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            ISemanaOperativaRepository semanaOperativaRepository,
            IColetaInsumoRepository coletaInsumoRepository,
            IGabaritoRepository gabaritoRepository,
            ISituacaoColetaInsumoRepository situacaoColetaInsumoRepository,
            IArquivoRepository arquivoRepository)
        {
            DadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            SemanaOperativaRepository = semanaOperativaRepository;
            ColetaInsumoRepository = coletaInsumoRepository;
            GabaritoRepository = gabaritoRepository;
            SituacaoColetaInsumoRepository = situacaoColetaInsumoRepository;
            ArquivoRepository = arquivoRepository;
        }

        public DadoColetaNaoEstruturado ObterPorChave(int chave)
        {
            return DadoColetaNaoEstruturadoRepository.FindByKey(chave);
        }
    }
}
