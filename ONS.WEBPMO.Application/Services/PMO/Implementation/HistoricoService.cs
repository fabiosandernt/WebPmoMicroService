namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    using ONS.WEBPMO.Application.Services.PMO.Interfaces;
    using System;
    using System.Collections.Generic;

    public class HistoricoService : Service, IHistoricoService
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

        public void ExcluirHistoricoColetaInsumo(int idColetaInsumo)
        {
            historicoColetaInsumoRepository.Delete(g => g.ColetaInsumo.Id == idColetaInsumo);
        }

        public void ExcluirHistoricoColetaInsumoViaSemanaOperativa(int idSemanaOperativa)
        {
            historicoColetaInsumoRepository.Delete(g => g.ColetaInsumo.SemanaOperativa.Id == idSemanaOperativa);
        }

        public void ExcluirHistoricoSemanaOperativa(int idSemanaOperativa)
        {
            historicoSemanaOperativaRepository.Delete(g => g.SemanaOperativa.Id == idSemanaOperativa);
        }

        public void ExcluirHistoricosSemanaOperativa(ISet<SemanaOperativa> semanasOperativas)
        {
            foreach (var semana in semanasOperativas)
            {
                ExcluirHistoricoSemanaOperativa(semana.Id);
            }
        }
    }
}
