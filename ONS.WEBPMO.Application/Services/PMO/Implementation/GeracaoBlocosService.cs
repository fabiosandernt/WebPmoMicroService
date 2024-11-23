using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{

    public class GeracaoBlocosService : IGeracaoBlocosService
    {
        private readonly IColetaInsumoService coletaInsumoService;

        private readonly ISemanaOperativaRepository semanaOperativaRepository;
        private readonly IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository;
        private readonly IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository;
        private readonly IDadoColetaManutencaoRepository dadoColetaManutencaoRepository;

        public GeracaoBlocosService(
            ISemanaOperativaRepository semanaOperativaRepository,
            IDadoColetaNaoEstruturadoRepository dadoColetaNaoEstruturadoRepository,
            IDadoColetaEstruturadoRepository dadoColetaEstruturadoRepository,
            IDadoColetaManutencaoRepository dadoColetaManutencaoRepository,
            IColetaInsumoService coletaInsumoService)
        {
            this.semanaOperativaRepository = semanaOperativaRepository;
            this.dadoColetaNaoEstruturadoRepository = dadoColetaNaoEstruturadoRepository;
            this.dadoColetaEstruturadoRepository = dadoColetaEstruturadoRepository;
            this.dadoColetaManutencaoRepository = dadoColetaManutencaoRepository;
            this.coletaInsumoService = coletaInsumoService;
        }

        public void GerarBlocos(int idSemanaOperativa, byte[] versao, bool somenteAprovados)
        {
            throw new NotImplementedException();
        }
    }
}
