using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoPQ : BlocoMontador
    {
        public BlocoPQ(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa) { }

        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(1, "&", adicaoEspaco: false)
                .ConfigurarCampoFixo(70, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(68, "&       BLOCO 9   GERAÇÃO EM PEQUENAS USINAS *** GERAÇÃO EXTERNA ***");

            ConfigurarRegistro("registro3")
                .ConfigurarCampoFixo(21, "&       (REGISTRO PQ)”");

            ConfigurarRegistro("registro4")
                .ConfigurarCampoFixo(1, "&", adicaoEspaco: false)
                .ConfigurarCampoFixo(70, "-", TipoDadoRegistro.Texto, Alinhamento.Esquerda, true);

            ConfigurarRegistro("registro5")
                .ConfigurarCampoFixo(39, "&                       PAT1|PAT2|PAT3|");

            ConfigurarRegistro("registro6")
                .ConfigurarCampoFixo(29, "&   NOME....   S  EST   VALOR");

            ConfigurarRegistro("registro7")
                .ConfigurarCampoFixo(39, "&   xxxxxxxxxxxX   xx   XXXXXxxxxxXXXXX");

            ConfigurarRegistro("registro8")
                .ConfigurarCampoFixo(3, "&PQ");

            ConfigurarRegistro("registro9")
                .ConfigurarCampoFixo(2, "PQ")
                .ConfigurarSeparador()
                .ConfigurarCampo(10)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarSeparador(2)
                .ConfigurarCampo(2, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarSeparador(3)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita);
        }

        protected override void ProcessarDadosBloco()
        {
            var dadosColeta = DadosColeta
                .Where(d => d is DadoColetaEstruturado
                    && d.Gabarito.OrigemColeta is Subsistema)
                .Cast<DadoColetaEstruturado>()
                .ToList();

            /* Agrupamento por Subsistema*/
            var dadosBlocoPorSubsistemaGroup = DadosColetaBloco
                .Where(d => d.Gabarito.OrigemColeta is Subsistema)
                .OrderBy(d => d.Estagio)
                .GroupBy(d => (Subsistema)d.Gabarito.OrigemColeta)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

            AdicionarRegistro("registro1");
            AdicionarRegistro("registro2");
            AdicionarRegistro("registro3");
            AdicionarRegistro("registro4");
            AdicionarRegistro("registro5");
            AdicionarRegistro("registro6");
            AdicionarRegistro("registro7");
            AdicionarRegistro("registro8");

            foreach (var dadosBlocoPorSubsistema in dadosBlocoPorSubsistemaGroup)
            {
                dadosBlocoPorSubsistema.Value.Sort();

                string idSubsistema = dadosBlocoPorSubsistema.Key.Id;

                /* Agrupamento por Estágio */
                var dadosBlocoPorEstagioGroup = dadosBlocoPorSubsistema.Value
                    .GroupBy(d => d.Estagio)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.ToList());

                int contadorEstagio = 1;

                foreach (var dadosBlocoPorEstagio in dadosBlocoPorEstagioGroup)
                {
                    string estagio = dadosBlocoPorEstagio.Key;
                    var dadosColetaEstagio = dadosColeta
                        .Where(d => d.Gabarito.OrigemColetaId == idSubsistema && d.Estagio == estagio)
                        .ToList();

                    AdicionarRegistro("registro9",
                        dadosBlocoPorSubsistema.Key.Nome,
                        dadosBlocoPorSubsistema.Key.Codigo,
                        contadorEstagio++,
                        ObterValorGrandeza(dadosColetaEstagio, 1, 1, "-", TipoPatamarEnum.Pesado),
                        ObterValorGrandeza(dadosColetaEstagio, 1, 1, "-", TipoPatamarEnum.Media),
                        ObterValorGrandeza(dadosColetaEstagio, 1, 1, "-", TipoPatamarEnum.Leve));
                }
            }
        }
    }
}
