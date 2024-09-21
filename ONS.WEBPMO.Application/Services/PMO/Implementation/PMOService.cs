using ONS.WEBPMO.Application.Services.PMO.Interfaces;
using System.Globalization;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation
{
    public class PMOService : Service, IPMOService
    {
        private readonly IPMORepository pmoRepository;
        private readonly ISemanaOperativaService semanaOperativaService;
        private readonly IParametroService parametroService;
        private readonly IHistoricoService historicoService;

        public PMOService(IPMORepository pmoRepository,
            ISemanaOperativaService semanaOperativaService,
            IParametroService parametroService,
            IHistoricoService historicoService)
        {
            this.pmoRepository = pmoRepository;
            this.semanaOperativaService = semanaOperativaService;
            this.parametroService = parametroService;
            this.historicoService = historicoService;
        }

        public void AtualizarMesesAdiantePMO(int idPMO, int? mesesAdiante, byte[] versao)
        {
            ValidarQuantidadeMesesAdiante(mesesAdiante);
            PMO pmo = pmoRepository.FindByKeyConcurrencyValidate(idPMO, versao);
            if (pmo != null)
            {
                pmo.Versao = versao;
                pmo.QuantidadeMesesAdiante = mesesAdiante;
            }
        }

        public PMO ObterPMOPorChave(int chave)
        {
            return pmoRepository.FindByKey(chave);
        }

        public PMO ObterPMOPorFiltro(PMOFilter filtro)
        {
            return pmoRepository.ObterPorFiltro(filtro);
        }

        public PMO ObterPMOPorFiltroExterno(PMOFilter filtro)
        {
            return pmoRepository.ObterPorFiltroExterno(filtro);
        }

        public void ExcluirPMO(DadosPMODTO dto)
        {
            PMO pmo = pmoRepository.FindByKeyConcurrencyValidate(dto.IdPMO, dto.VersaoPMO);
            if (pmo != null)
            {
                ValidarExclusaoPMO(pmo);
                pmo.Versao = dto.VersaoPMO;
                historicoService.ExcluirHistoricosSemanaOperativa(pmo.SemanasOperativas);
                pmoRepository.Delete(pmo);
            }
            else
            {
                throw new ONSBusinessException(SGIPMOMessages.MS014);
            }
        }

        public PMO GerarPMO(int ano, int mes)
        {
            ValidarInclusaoPMO(ano, mes);
            PMO pmo = new PMO
            {
                AnoReferencia = ano,
                MesReferencia = mes,
                SemanasOperativas = semanaOperativaService.GerarSugestaoSemanasOperativas(ano, mes)
            };
            var parametroQtdMeses = parametroService.ObterParametro(ParametroEnum.QuantidadeMesesAFrente);
            if (parametroQtdMeses != null)
            {
                pmo.QuantidadeMesesAdiante = int.Parse(parametroQtdMeses.Valor);
            }
            pmoRepository.Add(pmo);
            return pmo;
        }

        #region Incluir Semana Operativa

        public void IncluirSemanaOperativa(InclusaoSemanaOperativaDTO dto)
        {
            PMO pmo = pmoRepository.FindByKeyConcurrencyValidate(dto.IdPMO, dto.VersaoPMO);
            if (pmo != null)
            {
                pmo.Versao = dto.VersaoPMO; // Necessário para forçar o incremento da versão do PMO

                var semanasOrdenasPorRevisao = pmo.SemanasOperativas.OrderBy(s => s.Revisao);

                var cultura = CultureInfo.CurrentCulture;
                string nomeMes = cultura.TextInfo.ToTitleCase(cultura.DateTimeFormat.GetMonthName(pmo.MesReferencia));

                if (dto.IsInicioPMO)
                {
                    ValidarExisteEstudoPmo(pmo);
                    semanaOperativaService.AtualizarSemanasOperativasInclusao(semanasOrdenasPorRevisao,
                        pmo.AnoReferencia, nomeMes);
                }

                int revisao = ObterNumeroRevisao(pmo.SemanasOperativas.Count, dto.IsInicioPMO);
                DateTime dataInicioSemana = ObterDataInicioSemana(semanasOrdenasPorRevisao, dto.IsInicioPMO);

                ValidarDataInclusaoExclusaoSemanaOperativa(dataInicioSemana);

                DateTime dataFimPMO = ObterDataFimPMO(semanasOrdenasPorRevisao, dto.IsInicioPMO);

                SemanaOperativa semanaOperativa = semanaOperativaService.GerarSemanaOperativa(pmo.AnoReferencia,
                    nomeMes, dataInicioSemana, dataFimPMO, revisao);

                pmo.SemanasOperativas.Add(semanaOperativa);
            }
        }

        private int ObterNumeroRevisao(int ultimaRevisao, bool isInicioPMO)
        {
            int revisao = 0;
            if (!isInicioPMO)
            {
                revisao = ultimaRevisao;
            }
            return revisao;
        }

        private DateTime ObterDataInicioSemana(IEnumerable<SemanaOperativa> semanasOperativas, bool isInicioPMO)
        {
            return isInicioPMO ? semanasOperativas.First().DataInicioSemana.AddDays(-7)
                : semanasOperativas.Last().DataInicioSemana.AddDays(7);
        }

        private DateTime ObterDataFimPMO(IEnumerable<SemanaOperativa> semanasOperativas, bool isInicioPMO)
        {
            return isInicioPMO ? semanasOperativas.Last().DataFimSemana
                : semanasOperativas.Last().DataFimSemana.AddDays(7);
        }

        #endregion

        #region Excluir Semana Operativa
        public void ExcluirUltimaSemanaOperativa(int idPMO, byte[] versaoPMO)
        {
            PMO pmo = pmoRepository.FindByKeyConcurrencyValidate(idPMO, versaoPMO);
            if (pmo != null)
            {
                ValidarExistenciaSemanaOperativa(pmo);

                SemanaOperativa ultimaSemana = pmo.SemanasOperativas.Last();

                DateTime dataInicioUltimaSemana = ultimaSemana.DataInicioSemana;
                ValidarDataInclusaoExclusaoSemanaOperativa(dataInicioUltimaSemana);

                ValidarColetaDados(ultimaSemana);

                semanaOperativaService.ExcluirSemana(ultimaSemana);

                pmo.Versao = versaoPMO;
            }
        }

        #endregion

        #region Validação

        private void ValidarExisteEstudoPmo(PMO pmo)
        {
            if (pmo.SemanasOperativas.Any(s => s.Situacao != null
                && s.Situacao.Id != (int)SituacaoSemanaOperativaEnum.Configuracao))
            {
                throw new ONSBusinessException(SGIPMOMessages.MS065);
            }
        }

        private void ValidarInclusaoPMO(int ano, int mes)
        {
            IList<string> mensagens = new List<string>();

            DateTime dataCorrente = DateTime.Now.Date;
            if (dataCorrente.Year > ano
                || dataCorrente.Year == ano && dataCorrente.Month > mes)
            {
                mensagens.Add(SGIPMOMessages.MS003);
            }

            PMOFilter filtro = new PMOFilter()
            {
                Ano = ano,
                Mes = mes
            };

            PMO pmo = pmoRepository.ObterPorFiltro(filtro);

            if (pmo != null)
            {
                mensagens.Add(SGIPMOMessages.MS002);
            }
            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        private void ValidarExistenciaSemanaOperativa(PMO pmo)
        {
            if (pmo != null)
            {
                if (pmo.SemanasOperativas.Count == 1)
                {
                    throw new ONSBusinessException(SGIPMOMessages.MS007);
                }
            }
        }

        private void ValidarDataInclusaoExclusaoSemanaOperativa(DateTime data)
        {
            if (data.CompareTo(DateTime.Now.Date) < 0)
            {
                throw new ONSBusinessException(SGIPMOMessages.MS010);
            }
        }

        private void ValidarColetaDados(SemanaOperativa semanaOperativa)
        {
            var situacao = semanaOperativa.Situacao;
            if (situacao != null)
            {
                var sitaucaoSemanaOperativa = (SituacaoSemanaOperativaEnum)situacao.Id;
                if (sitaucaoSemanaOperativa >= SituacaoSemanaOperativaEnum.ColetaDados)
                {
                    throw new ONSBusinessException(SGIPMOMessages.MS011);
                }
            }
        }

        private void ValidarExclusaoPMO(PMO pmo)
        {
            IList<string> mensagens = new List<string>();

            bool existeSemanaPosConfiguracao =
                pmo.SemanasOperativas.Any(s => s.Situacao != null &&
                    s.Situacao.Id > (int)SituacaoSemanaOperativaEnum.Configuracao);

            if (existeSemanaPosConfiguracao)
            {
                mensagens.Add(SGIPMOMessages.MS029);
            }

            bool existeSemanaComGabarito =
                pmo.SemanasOperativas.Any(s => s.Gabaritos.Any());

            if (existeSemanaComGabarito)
            {
                mensagens.Add(SGIPMOMessages.MS030);
            }

            if (mensagens.Any())
            {
                throw new ONSBusinessException(mensagens);
            }
        }

        private void ValidarQuantidadeMesesAdiante(int? qtdMeses)
        {
            if (qtdMeses.HasValue && qtdMeses.Value > 11)
            {
                throw new ONSBusinessException(SGIPMOMessages.MS009);
            }
        }

        #endregion
    }
}
