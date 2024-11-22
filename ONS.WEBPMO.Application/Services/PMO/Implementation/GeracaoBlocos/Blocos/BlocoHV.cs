﻿using ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Enums;
using ONS.WEBPMO.Domain.Entities.PMO;
using ONS.WEBPMO.Domain.Entities.PMO.OrigemColetaPMO;
using ONS.WEBPMO.Domain.Enumerations;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos.Blocos
{
    public class BlocoHV : BlocoMontador
    {
        public BlocoHV(IList<DadoColeta> dadosColeta, IList<DadoColetaBloco> dadosColetaBloco, SemanaOperativa semanaOperativa)
            : base(dadosColeta, dadosColetaBloco, semanaOperativa) { }

        protected override void ConfigurarMapeamento()
        {
            ConfigurarRegistro("registro1")
                .ConfigurarCampoFixo(3, "&HV", adicaoEspaco: false)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita, adicaoEspaco: false)
                .ConfigurarCampoFixo(100, "-", conteudoRepetido: true);

            ConfigurarRegistro("registro2")
                .ConfigurarCampoFixo(5, "&CdRs")
                .ConfigurarCampoFixo(4, "CdUs", adicaoEspaco: false)
                .ConfigurarCampoFixo(20, "Nome da Usina", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(1, "E")
                .ConfigurarCampoFixo(10, "Lim. Inf.", TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampoFixo(10, "Lim. Sup.", TipoDadoRegistro.Texto, Alinhamento.Direita);

            ConfigurarRegistro("registro3")
                .ConfigurarCampoFixo(1, "&")
                .ConfigurarCampo(256);

            ConfigurarRegistro("registro4")
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(4, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(20, TipoDadoRegistro.Texto, Alinhamento.Direita)
                .ConfigurarCampo(1, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampo(10, TipoDadoRegistro.Numero, Alinhamento.Direita);

            ConfigurarRegistro("registro5")
                .ConfigurarCampoFixo(3, "&HV", adicaoEspaco: false)
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

            int quantidadeUsinaReservatorio = dadosColetaList
                .Where(d => !string.IsNullOrWhiteSpace(d.Valor))
                .GroupBy(d => d.Gabarito.OrigemColetaId).Count();

            AdicionarRegistro("registro1", quantidadeUsinaReservatorio);
            AdicionarRegistro("registro2");

            foreach (var dadosBlocoPorUsina in dadosBlocoPorUsinaGroup)
            {
                dadosBlocoPorUsina.Value.Sort();

                string codigoDpp = dadosBlocoPorUsina.Key.CodigoDPP.ToString();
                string nomeUsina = dadosBlocoPorUsina.Key.NomeCurto;
                string codigoRestricao = codigoDpp;
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

                    /* Gerar para usina apenas se existir dado coletado */
                    if (!dadosColetaList.Any(d => d.Gabarito.OrigemColetaId == idUsina
                        && d.ColetaInsumo.InsumoId == idInsumo
                        && !string.IsNullOrWhiteSpace(d.Valor)))
                    {
                        continue;
                    }

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

                        var dadosColetaEstagio = dadosColetaInsumo
                            .Where(d => d.Estagio == estagio)
                            .ToList();

                        AdicionarRegistro("registro4",
                            codigoRestricao, codigoDpp, nomeUsina, contadorEstagio++,
                            ObterValorGrandeza(dadosColetaEstagio, 2, i, "-", limite: TipoLimiteEnum.Inferior),
                            ObterValorGrandeza(dadosColetaEstagio, 2, i, "-", limite: TipoLimiteEnum.Superior)
                        );
                    }
                }
            }

            AdicionarRegistro("registro5");
        }
    }
}
