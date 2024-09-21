namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Enums;

    public class BlocoOutras : BlocoMontador
    {
        public BlocoOutras(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa) { }

        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(7, "&Outras", adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(30, "Potência Contratada de Itaipu:")
                .ConfigurarCampo(5, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(2, "MW");

            ConfigurarRegistro("registro3")
                .ConfigurarCampoFixo(48, "& Suprimento de Energia e Demanda à ANDE (MWmed)");

            ConfigurarRegistro("registro4")
                .ConfigurarCampoFixo(3, "& E")
                .ConfigurarCampoFixo(10, "Pesada", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Média", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Leve", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Semanal", TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro5")
                .ConfigurarSeparador(2)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro6")
                .ConfigurarCampoFixo(46, "& **** Consumo Interno de Energia (MWmed) ****");

            ConfigurarRegistro("registro7")
                .ConfigurarCampoFixo(3, "& E", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Pesada", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Média", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Leve", TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Hor. Ponta", TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro8")
                .ConfigurarSeparador(2)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro9")
                .ConfigurarCampoFixo(7, "&Outras", adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);
        }

        protected override void ProcessarDadosBloco()
        {
            var dadosColeta = DadosColeta
                .Where(d => d is DadoColetaEstruturado && d.Gabarito.OrigemColeta is IConjuntoGerador)
                .Cast<DadoColetaEstruturado>()
                .ToList();

            /* Agrupamento por Usina */
            var dadosBlocoPorUsinaGroup = DadosColetaBloco
                .Where(d => d.Gabarito.OrigemColeta is IConjuntoGerador)
                .OrderBy(d => ((IConjuntoGerador)d.Gabarito.OrigemColeta).CodigoDPP)
                .ThenBy(d => d.Estagio)
                .ThenBy(d => d.Grandeza.OrdemBlocoMontador)
                .GroupBy(d => (IConjuntoGerador)d.Gabarito.OrigemColeta)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

            AdicionarRegistro("registro1");

            var dadoPotenciaContratada = dadosColeta
                .FirstOrDefault(d => d.Grandeza.OrdemBlocoMontador == 1
                    && d.Grandeza.Insumo.OrdemBlocoMontador == 1);

            AdicionarRegistro("registro2", ObterValorGrandeza(dadoPotenciaContratada));
            AdicionarRegistro("registro3");
            AdicionarRegistro("registro4");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                dadosBlocoPorUsina.Value.Sort();

                string idUsina = dadosBlocoPorUsina.Key.Id;

                /* Agrupamento por Estágio */
                var dadosBlocoPorEstagioGroup = dadosBlocoPorUsina.Value
                    .GroupBy(d => d.Estagio)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                int contadorEstagio = 1;

                foreach (var dadosBlocoPorEstagio in dadosBlocoPorEstagioGroup)
                {
                    string estagio = dadosBlocoPorEstagio.Key;
                    var dadosColetaEstagio = dadosColeta
                        .Where(d => d.Estagio == estagio && d.Gabarito.OrigemColetaId == idUsina)
                        .ToList();

                    AdicionarRegistro("registro5",
                        contadorEstagio++,
                        ObterValorGrandeza(dadosColetaEstagio, 1, 2, "-", TipoPatamarEnum.Pesado),
                        ObterValorGrandeza(dadosColetaEstagio, 1, 2, "-", TipoPatamarEnum.Media),
                        ObterValorGrandeza(dadosColetaEstagio, 1, 2, "-", TipoPatamarEnum.Leve),
                        ObterValorGrandeza(dadosColetaEstagio, 2, 2, "-")
                    );
                }
            }

            AdicionarRegistro("registro6");
            AdicionarRegistro("registro7");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                dadosBlocoPorUsina.Value.Sort();

                string idUsina = dadosBlocoPorUsina.Key.Id;

                /* Agrupamento por Estágio */
                var dadosBlocoPorEstagioGroup = dadosBlocoPorUsina.Value
                    .GroupBy(d => d.Estagio)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                int contadorEstagio = 1;

                foreach (var dadosBlocoPorEstagio in dadosBlocoPorEstagioGroup)
                {
                    string estagio = dadosBlocoPorEstagio.Key;
                    var dadosColetaEstagio = dadosColeta
                        .Where(d => d.Estagio == estagio && d.Gabarito.OrigemColetaId == idUsina)
                        .ToList();

                    AdicionarRegistro("registro8",
                        contadorEstagio++,
                        ObterValorGrandeza(dadosColetaEstagio, 1, 3, "-", TipoPatamarEnum.Pesado),
                        ObterValorGrandeza(dadosColetaEstagio, 1, 3, "-", TipoPatamarEnum.Media),
                        ObterValorGrandeza(dadosColetaEstagio, 1, 3, "-", TipoPatamarEnum.Leve),
                        ObterValorGrandeza(dadosColetaEstagio, 2, 3, "-")
                    );
                }
            }

            AdicionarRegistro("registro9");
        }
    }
}
