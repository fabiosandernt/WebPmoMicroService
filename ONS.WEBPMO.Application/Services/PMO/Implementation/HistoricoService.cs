using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Repository;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
   

    public class HistoricoService :  IHistoricoService
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
            HistoricoColetaInsumo historico = new HistoricoColetaInsumo();
            historico.ColetaInsumo = coletaInsumo;
            historico.Situacao = coletaInsumo.Situacao;
            historico.DataHoraAlteracao = DateTime.Now;

            historicoColetaInsumoRepository.Add(historico, false);
        }

        public void CriarSalvarHistoricoSemanaOperativa(SemanaOperativa semanaOperativa)
        {
            HistoricoSemanaOperativa historico = new HistoricoSemanaOperativa();
            historico.SemanaOperativa = semanaOperativa;
            historico.Situacao = semanaOperativa.Situacao;
            historico.DataHoraAlteracao = DateTime.Now;

            historicoSemanaOperativaRepository.Add(historico, false);
        }

        //public void ExcluirHistoricoColetaInsumo(int idColetaInsumo)
        //{
        //    historicoColetaInsumoRepository.Delete(g => g.ColetaInsumo.Id == idColetaInsumo);
        //}
        public void ExcluirHistoricoColetaInsumo(int idColetaInsumo)
        {
            var registrosParaExcluir = historicoColetaInsumoRepository.GetAll()
                .Where(h => h.ColetaInsumoId == idColetaInsumo)
                .ToList();

            foreach (var registro in registrosParaExcluir)
            {
                historicoColetaInsumoRepository.Delete(registro);
            }
        }
        public void ExcluirHistoricoColetaInsumoViaSemanaOperativa(int idSemanaOperativa)
        {
            var registrosParaExcluir = historicoColetaInsumoRepository.GetAll()
                .Where(h => h.ColetaInsumo.SemanaOperativa.Id == idSemanaOperativa)
                .ToList();

            foreach (var registro in registrosParaExcluir)
            {
                historicoColetaInsumoRepository.Delete(registro);
            }
        }

        //public void ExcluirHistoricoColetaInsumoViaSemanaOperativa(int idSemanaOperativa)
        //{
        //    historicoColetaInsumoRepository.Delete(g => g.ColetaInsumo.SemanaOperativa.Id == idSemanaOperativa);
        //}
        public void ExcluirHistoricoSemanaOperativa(int idSemanaOperativa)
        {
            var registrosParaExcluir = historicoSemanaOperativaRepository.GetAll()
                .Where(h => h.SemanaOperativa.Id == idSemanaOperativa)
                .ToList();

            foreach (var registro in registrosParaExcluir)
            {
                historicoSemanaOperativaRepository.Delete(registro);
            }
        }

        //public void ExcluirHistoricoSemanaOperativa(int idSemanaOperativa)
        //{
        //    historicoSemanaOperativaRepository.Delete(g => g.SemanaOperativa.Id == idSemanaOperativa);
        //}

        public void ExcluirHistoricosSemanaOperativa(ISet<SemanaOperativa> semanasOperativas)
        {
            foreach (var semana in semanasOperativas)
            {
                ExcluirHistoricoSemanaOperativa(semana.Id);
            }
        }
    }
}
