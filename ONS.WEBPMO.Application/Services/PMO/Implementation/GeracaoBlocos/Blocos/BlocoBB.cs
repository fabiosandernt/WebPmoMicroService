namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using Entities;
    using System.Collections.Generic;
    using System.Linq;
    using Enums;

    public class BlocoBB : BlocoMontador
    {
        public BlocoBB(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa)
        {
        }


        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(3, "&BB", adicaoEspaco: false)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita, adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", conteudoRepetido: true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(5, "&CdRs")
                .ConfigurarCampoFixo(4, "CdUs", adicaoEspaco: false)
                .ConfigurarCampoFixo(20, "Nome da Usina", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(1, "E")
                .ConfigurarCampoFixo(10, "Pesada", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Média", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Leve", TipoDadoRegistro.Texto, Alinhamento.Direita);

            ConfigurarRegistro("registro3")
                .ConfigurarCampoFixo(1, "&")
                .ConfigurarCampo(255);

            ConfigurarRegistro("registro4")
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(20, TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro5")
                .ConfigurarCampoFixo(3, "&BB", adicaoEspaco: false)
                .ConfigurarCampoFixo(104, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);
        }

        protected override void ProcessarDadosBloco()
        {
            var dadosColetaList = DadosColeta
                .Where(d => d is DadoColetaEstruturado
                    && d.Gabarito.OrigemColeta is IConjuntoGerador)
                .Cast<DadoColetaEstruturado>()
                .ToList();

            /* Agrupamento por Usina/Reservatório */
            var dadosBlocoPorUsinaGroup = DadosColetaBloco
                .Where(d => d.Gabarito.OrigemColeta is IConjuntoGerador)
                .OrderBy(d => ((IConjuntoGerador)d.Gabarito.OrigemColeta).CodigoDPP)
                .ThenBy(d => d.Estagio)
                .ThenBy(d => d.Grandeza.OrdemBlocoMontador)
                .GroupBy(d => (IConjuntoGerador)d.Gabarito.OrigemColeta)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

            AdicionarRegistro("registro1", dadosBlocoPorUsinaGroup.Count());
            AdicionarRegistro("registro2");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                dadosBlocoPorUsina.Value.Sort();

                string codigoDpp = dadosBlocoPorUsina.Key.CodigoDPP.ToString();
                string nomeUsina = dadosBlocoPorUsina.Key.NomeCurto;
                string codigoRestricao = codigoDpp;
                string idUsina = dadosBlocoPorUsina.Key.Id;
                int contadorEstagio = 1;

                var dadosColetaUsina = dadosColetaList
                    .Where(d => d.Gabarito.OrigemColetaId == idUsina)
                    .ToList();

                var dadosBlocoPorInsumoGroup = dadosBlocoPorUsina.Value
                    .GroupBy(d => d.Insumo)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                foreach (var dadosBlocoPorInsumo in dadosBlocoPorInsumoGroup)
                {
                    int idInsumo = dadosBlocoPorInsumo.Key.Id;

                    /* Agrupamento por Estágio */
                    var dadosPorEstagioGroup = dadosBlocoPorInsumo.Value
                        .GroupBy(d => d.Estagio)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                    var dadosColetaInsumo = dadosColetaUsina
                        .Where(d => d.ColetaInsumo.InsumoId == idInsumo)
                        .ToList();

                    AdicionarRegistro("registro3", ObterValorGrandeza(dadosColetaInsumo, 1, 1, ""));

                    foreach (var dadosBlocoPorEstagio in dadosPorEstagioGroup)
                    {
                        string estagio = dadosBlocoPorEstagio.Key;

                        var dadosColetaEstagio = dadosColetaInsumo
                            .Where(d => d.Estagio == estagio)
                            .ToList();

                        AdicionarRegistro("registro4",
                            codigoRestricao, codigoDpp, nomeUsina, contadorEstagio++,
                            ObterValorGrandeza(dadosColetaEstagio, 2, 1, "-", TipoPatamarEnum.Pesado),
                            ObterValorGrandeza(dadosColetaEstagio, 2, 1, "-", TipoPatamarEnum.Media),
                            ObterValorGrandeza(dadosColetaEstagio, 2, 1, "-", TipoPatamarEnum.Leve)
                        );
                    }
                }
            }

            AdicionarRegistro("registro5");

        }
    }
}
