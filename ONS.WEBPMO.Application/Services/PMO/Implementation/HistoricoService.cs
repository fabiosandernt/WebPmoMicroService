using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository.PMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{


    public class HistoricoService : IHistoricoService
    {
        private readonly IHistoricoColetaInsumoRepository historicoColetaInsumoRepository;
        private readonly IHistoricoSemanaOperativaRepository historicoSemanaOperativaRepository;

        public HistoricoService(
            IHistoricoColetaInsumoRepository historicoColetaInsumoRepository,
            IHistoricoSemanaOperativaRepository historicoSemanaOperativaRepository)
        {
            this.historicoColetaInsumoRepository = historicoColetaInsumoRepository;
            this.historicoSemanaOperativaRepository = historicoSemanaOperativaRepository;
        }

        public void CriarSalvarHistoricoColetaInsumo(ColetaInsumo coletaInsumo)
        {
            throw new NotImplementedException();
        }

        public void CriarSalvarHistoricoSemanaOperativa(SemanaOperativa semanaOperativa)
        {
            throw new NotImplementedException();
        }

        public void ExcluirHistoricoColetaInsumo(int idColetaInsumo)
        {
            throw new NotImplementedException();
        }

        public void ExcluirHistoricoColetaInsumoViaSemanaOperativa(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public void ExcluirHistoricoSemanaOperativa(int idSemanaOperativa)
        {
            throw new NotImplementedException();
        }

        public void ExcluirHistoricosSemanaOperativa(ISet<SemanaOperativa> idsSemanaOperativa)
        {
            throw new NotImplementedException();
        }
    }
}
