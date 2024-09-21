namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Enums;

    public abstract class BlocoCadastroTermica : BlocoMontador
    {
        private readonly TipoBlocoEnum tipoBloco;

        public BlocoCadastroTermica(TipoBlocoEnum tipoBloco, IList<DadoColeta> dadosColeta,
            IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa)
        {
            this.tipoBloco = tipoBloco;
        }

        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(3, string.Format("&{0}", tipoBloco), adicaoEspaco: false)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita, adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", conteudoRepetido: true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(3, "&US")
                .ConfigurarCampoFixo(2, "SS")
                .ConfigurarCampoFixo(20, "Nome da Usina", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(1, "E")
                .ConfigurarCampoFixo(10, "CVU", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Disp. Pes.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Infx. Pes.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Disp. Med.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Infx. Med.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Disp. Lev.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Infx. Lev.", TipoDadoRegistro.Texto, Alinhamento.Direita);

            ConfigurarRegistro("registro3")
                .ConfigurarCampoFixo(1, "&", adicaoEspaco: false)
                .ConfigurarCampo(256);

            ConfigurarRegistro("registro4")
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(20, TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro5")
                .ConfigurarCampoFixo(3, string.Format("&{0}", tipoBloco), adicaoEspaco: false)
                .ConfigurarCampoFixo(104, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);
        }

        protected override void ProcessarDadosBloco()
        {
            var dadosColetaList = DadosColeta
                .Where(d => d is DadoColetaEstruturado && d.Gabarito.OrigemColeta is Usina)
                .Cast<DadoColetaEstruturado>()
                .ToList();

            /* Agrupamento por Usina */
            var dadosBlocoPorUsinaGroup = DadosColetaBloco
                .Where(d => d.Gabarito.OrigemColeta is Usina)
                .OrderBy(d => ((Usina)d.Gabarito.OrigemColeta).CodigoDPP)
                .ThenBy(d => d.Estagio)
                .ThenBy(d => d.Grandeza.OrdemBlocoMontador)
                .GroupBy(d => (Usina)d.Gabarito.OrigemColeta)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

            AdicionarRegistro("registro1", dadosBlocoPorUsinaGroup.Count());
            AdicionarRegistro("registro2");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                dadosBlocoPorUsina.Value.Sort();

                string codigoDpp = dadosBlocoPorUsina.Key.CodigoDPP.ToString();
                string nomeUsina = dadosBlocoPorUsina.Key.NomeCurto;
                string codigoSubsistema = dadosBlocoPorUsina.Key.Subsistema.Codigo;
                string idUsina = dadosBlocoPorUsina.Key.Id;

                var dadosColetaUsina = dadosColetaList
                    .Where(d => d.Gabarito.OrigemColetaId == idUsina)
                    .ToList();

                var dadosBlocoPorInsumoGroup = dadosBlocoPorUsina.Value
                    .GroupBy(d => d.Insumo)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                foreach (var dadosBlocoPorInsumo in dadosBlocoPorInsumoGroup)
                {
                    /* Ordem/Posição do insumo no bloco */
                    int i = dadosBlocoPorInsumo.Key.OrdemBlocoMontador ?? 1;
                    int idInsumo = dadosBlocoPorInsumo.Key.Id;
                    int contadorEstagio = 1;

                    /* Agrupamento por Estágio */
                    var dadosPorEstagioGroup = dadosBlocoPorInsumo.Value
                        .GroupBy(d => d.Estagio)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                    var dadosColetaInsumo = dadosColetaUsina
                        .Where(d => d.ColetaInsumo.InsumoId == idInsumo)
                        .ToList();

                    AdicionarRegistro("registro3", ObterValorGrandeza(dadosColetaInsumo, 1, i, ""));

                    foreach (var dadosBlocoPorEstagio in dadosPorEstagioGroup)
                    {
                        string estagio = dadosBlocoPorEstagio.Key;

                        var dadosColeta = dadosColetaInsumo
                            .Where(d => d.Estagio == estagio)
                            .ToList();

                        AdicionarRegistro("registro4",
                            codigoDpp, codigoSubsistema, nomeUsina, contadorEstagio++,
                            ObterValorGrandeza(dadosColeta, 2, i),
                            ObterValorGrandeza(dadosColeta, 3, i, "-", TipoPatamarEnum.Pesado),
                            ObterValorGrandeza(dadosColeta, 4, i, "-", TipoPatamarEnum.Pesado),
                            ObterValorGrandeza(dadosColeta, 3, i, "-", TipoPatamarEnum.Media),
                            ObterValorGrandeza(dadosColeta, 4, i, "-", TipoPatamarEnum.Media),
                            ObterValorGrandeza(dadosColeta, 3, i, "-", TipoPatamarEnum.Leve),
                            ObterValorGrandeza(dadosColeta, 4, i, "-", TipoPatamarEnum.Leve)
                        );
                    }
                }
            }

            AdicionarRegistro("registro5");
        }
    }
}
