namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using System.Linq;

    using Entities;
    using Enums;

    public class BlocoUH : BlocoMontador
    {
        public BlocoUH(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa) { }

        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(3, "&UH", adicaoEspaco: false)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita, adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(5, "&CdUS")
                .ConfigurarCampoFixo(1, "S")
                .ConfigurarCampoFixo(6, "VUtil", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "VT. Max.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Ger. Max.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(100, "Nome Usina");

            ConfigurarRegistro("registro3")
                .ConfigurarCampo(5, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(6, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(100);

            ConfigurarRegistro("registro4")
                .ConfigurarCampoFixo(3, "&UH", adicaoEspaco: false)
                .ConfigurarCampoFixo(104, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);
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

            AdicionarRegistro("registro1", dadosBlocoPorUsinaGroup.Count());
            AdicionarRegistro("registro2");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                IList<DadoColetaEstruturado> dadosPorGrandeza = new List<DadoColetaEstruturado>();
                if (dadosPorUsinaGroup.ContainsKey(dadosBlocoPorUsina.Key))
                {
                    dadosPorGrandeza = dadosPorUsinaGroup[dadosBlocoPorUsina.Key]
                        .OrderBy(d => d.Grandeza.OrdemBlocoMontador)
                        .ToList();
                }

                AdicionarRegistro("registro3",
                    dadosBlocoPorUsina.Key.CodigoDPP,
                    dadosBlocoPorUsina.Key.Subsistema.Codigo,
                    ObterValorGrandeza(dadosPorGrandeza, 1, 1),
                    ObterValorGrandeza(dadosPorGrandeza, 2, 1),
                    ObterValorGrandeza(dadosPorGrandeza, 3, 1),
                    dadosBlocoPorUsina.Key.NomeCurto);
            }

            AdicionarRegistro("registro4");
        }
    }
}