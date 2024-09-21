using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoMP : BlocoMontador
    {
        public BlocoMP(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa) { }

        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(3, "&MP", adicaoEspaco: false)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita, adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(5, "&CdUS")
                .ConfigurarCampoFixo(1, "T")
                .ConfigurarCampoFixo(20, "Nome da Usina", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(18, "Conj. & Maq.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Pot. Nom.", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Dt. Ini.", TipoDadoRegistro.Data, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Dt. Fim", TipoDadoRegistro.Data, Alinhamento.Direita)
                .ConfigurarCampoFixo(250, "Descrição");

            ConfigurarRegistro("registro3")
                .ConfigurarCampo(5, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(1, TipoDadoRegistro.Texto, Alinhamento.Esquerda)
                .ConfigurarCampo(20, TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampo(18, TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Data, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Data, Alinhamento.Direita)
                .ConfigurarCampo(250, TipoDadoRegistro.Texto, Alinhamento.Esquerda);

            ConfigurarRegistro("registro4")
                .ConfigurarCampoFixo(3, "&MP", adicaoEspaco: false)
                .ConfigurarCampoFixo(104, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);
        }

        protected override void ProcessarDadosBloco()
        {
            var dadosColeta = DadosColeta
                .Where(d => d is DadoColetaManutencao)
                .Cast<DadoColetaManutencao>()
                .ToList();

            var dadosConsolidados = ConsolidarDados(dadosColeta)
                .OrderBy(d => d.UnidadeGeradora.Usina.TipoUsina)
                .ThenBy(d => d.UnidadeGeradora.Usina.CodigoDPP)
                .ThenBy(d => d.UnidadeGeradora.Nome)
                .ThenBy(d => d.DataInicio);

            AdicionarRegistro("registro1", dadosConsolidados.Count());
            AdicionarRegistro("registro2");

            SemanaOperativa semanaOperativa = dadosConsolidados.First().ColetaInsumo.SemanaOperativa;

            foreach (var dado in dadosConsolidados)
            {
                UnidadeGeradora unidadeGeradora = dado.UnidadeGeradora;
                Usina usina = unidadeGeradora.Usina;
                string siglaTipoUsina = usina.TipoUsina == TipoUsinaEnum.Termica.ToDescription() ? "T" : "H";

                TimeSpan horaPicoInicio = dado.DataInicio.DayOfWeek == DayOfWeek.Sunday
                    ? new TimeSpan(0, 0, 0)
                    : new TimeSpan(18, 0, 0);

                TimeSpan horaPicoTermino = dado.DataInicio.DayOfWeek == DayOfWeek.Sunday
                    ? new TimeSpan(23, 59, 0)
                    : new TimeSpan(21, 59, 0);

                DateTime dataInicio = dado.DataInicio;
                DateTime dataFim = dado.DataFim;

                if (dado.DataInicio < semanaOperativa.DataInicioManutencao)
                {
                    dataInicio = semanaOperativa.DataInicioManutencao;
                }
                else if (dado.DataInicio.TimeOfDay > horaPicoInicio)
                {
                    dataInicio = dado.DataInicio.AddDays(1);
                }

                if (dado.DataFim > semanaOperativa.DataFimManutencao)
                {
                    dataFim = semanaOperativa.DataFimManutencao;
                }
                else if (dado.DataFim.TimeOfDay < horaPicoTermino)
                {
                    dataFim = dado.DataFim.AddDays(-1);
                }

                if (dataInicio <= dataFim)
                {
                    AdicionarRegistro("registro3",
                        usina.CodigoDPP,
                        siglaTipoUsina,
                        usina.NomeCurto,
                        string.Format("Conj. {0} - Maq. {1}", unidadeGeradora.NumeroConjunto,
                            unidadeGeradora.NumeroMaquina),
                        unidadeGeradora.PotenciaNominal,
                        dataInicio.ToString("dd/MM/yyyy"),
                        dataFim.ToString("dd/MM/yyyy"),
                        dado.Justificativa);
                }
            }

            AdicionarRegistro("registro4");
        }

        private IList<DadoColetaManutencao> ConsolidarDados(IList<DadoColetaManutencao> dadosManutencao)
        {
            IList<DadoColetaManutencao> dadosConsolidados = new List<DadoColetaManutencao>();
            foreach (DadoColetaManutencao dado in dadosManutencao)
            {
                DadoColetaManutencao dadoConsolidado = new DadoColetaManutencao(dado);

                /* Verifica se existedado dados que sobreponham o período de manutenção */
                var dadosAConsolidar = dadosConsolidados
                    .Where(d => d.UnidadeGeradora.Id == dado.UnidadeGeradora.Id
                        && dado.DataInicio <= d.DataFim
                        && dado.DataFim >= d.DataInicio)
                    .ToList();

                if (dadosAConsolidar.Any())
                {
                    string justificativas = string.Empty;

                    /* Remove dados com sobreposição de período */
                    foreach (DadoColetaManutencao consolidado in dadosAConsolidar)
                    {
                        justificativas += string.Format(" | {0}", consolidado.Justificativa);
                        dadosConsolidados.Remove(consolidado);
                    }

                    DateTime dataInicio = dadosAConsolidar.Min(d => d.DataInicio);
                    DateTime dataTermino = dadosAConsolidar.Max(d => d.DataFim);

                    /* Atribui a menor data inicial e a maior data termino dos dados em sobreposição */
                    dadoConsolidado.DataInicio = dado.DataInicio < dataInicio ? dado.DataInicio : dataInicio;
                    dadoConsolidado.DataFim = dado.DataFim > dataTermino ? dado.DataFim : dataTermino;
                    dadoConsolidado.Justificativa += justificativas;
                }

                dadosConsolidados.Add(dadoConsolidado);
            }

            return dadosConsolidados;
        }
    }
}