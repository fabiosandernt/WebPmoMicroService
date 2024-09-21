using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoJusmed : BlocoMontador
    {
        public BlocoJusmed(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa) { }

        protected override void ConfigurarMapeamento()
        {
            var dataReferencia = new DateTime(SemanaOperativa.PMO.AnoReferencia,
                SemanaOperativa.PMO.MesReferencia, 1);

            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(7, "&JUSMED", adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(31, string.Format("&CdUS {0} (m) {1} (m)",
                    dataReferencia.ToString("MMM/yyyy"),
                    dataReferencia.AddMonths(1).ToString("MMM/yyyy")));

            ConfigurarRegistro("registro3")
                .ConfigurarCampo(5, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro4")
                .ConfigurarCampoFixo(7, "&JUSMED", adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);
        }

        protected override void ProcessarDadosBloco()
        {
            var dadosPorUsinaGroup = DadosColeta.Where(d => d is DadoColetaEstruturado
                    && d.Gabarito.OrigemColeta is IConjuntoGerador)
                .OrderBy(d => ((IConjuntoGerador)d.Gabarito.OrigemColeta).CodigoDPP)
                .GroupBy(d => (IConjuntoGerador)d.Gabarito.OrigemColeta)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Cast<DadoColetaEstruturado>());

            var dadosBlocoPorUsinaGroup = DadosColetaBloco.Where(d => d.Gabarito.OrigemColeta is IConjuntoGerador)
                .OrderBy(d => ((IConjuntoGerador)d.Gabarito.OrigemColeta).CodigoDPP)
                .GroupBy(d => (IConjuntoGerador)d.Gabarito.OrigemColeta)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

            AdicionarRegistro("registro1");
            AdicionarRegistro("registro2");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                IList<DadoColetaEstruturado> dadosPorUsina = new List<DadoColetaEstruturado>();
                if (dadosPorUsinaGroup.ContainsKey(dadosBlocoPorUsina.Key))
                {
                    dadosPorUsina = dadosPorUsinaGroup[dadosBlocoPorUsina.Key]
                        .OrderBy(d => d.Grandeza.OrdemBlocoMontador)
                        .ToList();
                }

                AdicionarRegistro("registro3",
                    dadosBlocoPorUsina.Key.CodigoDPP,
                    ObterValorGrandeza(dadosPorUsina, 1),
                    ObterValorGrandeza(dadosPorUsina, 2));
            }

            AdicionarRegistro("registro4");
        }
    }
}