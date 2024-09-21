using ONS.Common.Util;

namespace ONS.WEBPMO.Application.Services.PMO.Implementation.GeracaoBlocos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Entities;
    using Blocos;
    using Enums;
    using Metadados;

    public class ArquivoMontador
    {
        public string Nome { get; set; }
        public Agente Agente { get; set; }
        public string CodigoPerfilOns { get; set; }
        public SemanaOperativa SemanaOperativa { get; set; }
        public IList<DadoColeta> DadosColetas { get; set; }
        public IList<DadoColetaBloco> DadosColetaBloco { get; set; }
        public bool SomenteAprovados { get; set; }


        public ArquivoMontador(
            Agente agente,
            string codigoPerfilOns,
            SemanaOperativa semanaOperativa,
            IList<DadoColeta> dadosColetas,
            IList<DadoColetaBloco> dadosColetaBloco,
            bool somenteAprovados)
        {
            Agente = agente;
            SemanaOperativa = semanaOperativa;
            DadosColetas = dadosColetas;
            DadosColetaBloco = dadosColetaBloco;
            CodigoPerfilOns = codigoPerfilOns;
            SomenteAprovados = somenteAprovados;
            GerarNomeArquivo();
        }

        public string GerarArquivo()
        {
            StringBuilder sbArquivo = new StringBuilder();
            GerarIdentificadorArquivo(sbArquivo);
            GerarBlocos(sbArquivo);
            return sbArquivo.ToString();
        }

        private void GerarIdentificadorArquivo(StringBuilder sbOutput)
        {
            RegistroConfiguracao configuracao = new RegistroConfiguracao()
                .ConfigurarCampoFixo(5, Agente.Id, TipoDadoRegistro.Numero, Alinhamento.Direita)
                .ConfigurarCampoFixo(19, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                .ConfigurarCampoFixo(10, string.Format("01/{0:00}/{1:0000}", SemanaOperativa.PMO.MesReferencia, SemanaOperativa.PMO.AnoReferencia))
                .ConfigurarCampoFixo(10, SemanaOperativa.DataInicioSemana.ToString("dd/MM/yyyy HH:mm:ss"))
                .ConfigurarCampoFixo(1, SemanaOperativa.Revisao)
                .ConfigurarCampoFixo(1, SemanaOperativa.PMO.SemanasOperativas.Count - SemanaOperativa.Revisao + SemanaOperativa.PMO.QuantidadeMesesAdiante)
                .ConfigurarCampoFixo(100, DadosColetaBloco.Any(dado => dado.Insumo.TipoBloco == TipoBlocoEnum.TG.ToString()) ? "GNL" : Agente.Nome);

            RegistroBloco cabecalhoIdentificador = new RegistroBloco("cabecalho", configuracao);

            cabecalhoIdentificador.ToString(sbOutput);
        }

        private void GerarBlocos(StringBuilder sbOutput)
        {
            var types = Assembly
                .GetAssembly(typeof(BlocoMontador))
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BlocoMontador)));

            foreach (var type in types)
            {
                string tipoBloco = type.Name.Substring(5).ToUpper();

                IList<DadoColeta> dados = DadosColetas
                    .Where(d => ((InsumoEstruturado)d.ColetaInsumo.Insumo).TipoBloco.ToUpper() == tipoBloco)
                    .ToList();

                IList<DadoColetaBloco> dadosBloco = DadosColetaBloco
                    .Where(d => d.Insumo.TipoBloco.ToUpper() == tipoBloco)
                    .ToList();

                var semanaOperativa = SemanaOperativa;

                if (SomenteAprovados)
                {
                    //IList<DadoColeta> insumosAgropados = dados
                    //                  .GroupBy(p => p.ColetaInsumo.Id)
                    //                  .Select(g => g.First())
                    //                  .ToList();

                    dados = dados.Where(x => x.ColetaInsumo.SituacaoId == 5).ToList();
                    dadosBloco = dadosBloco.Where(x => x.ColetaInsumo.SituacaoId == 5).ToList();

                    // Código comentado Bug 42508 Insumos aprovados não estão sendo gerados na funcionalidade de geração de blocos (Ex. Manutenção Hidroelétrica e Térmica para o mesmo agente)
                    //if (insumosAgropados.Count() > 1)
                    //{
                    //    //Todos os insumos agrupados precisam estar aprovados.
                    //    if (insumosAgropados.Count != insumosAgropados.Where(x => x.ColetaInsumo.SituacaoId == 5).Count())
                    //    {
                    //        foreach (var item in insumosAgropados)
                    //        {
                    //            dados = dados.Where(x => x.ColetaInsumo.Id != item.ColetaInsumo.Id).ToList();
                    //            dadosBloco = dadosBloco.Where(x => x.ColetaInsumo.Id != item.ColetaInsumo.Id).ToList();
                    //        }
                    //    }
                    //}
                }
                if (IsGerarArquivo(tipoBloco, dados, dadosBloco))
                {
                    BlocoMontador bloco = (BlocoMontador)Activator.CreateInstance(type, dados, dadosBloco, semanaOperativa);
                    bloco.ToString(sbOutput);
                }
            }
        }

        private bool IsGerarArquivo(string tipoBloco, IList<DadoColeta> dados, IList<DadoColetaBloco> dadosBloco)
        {
            IList<string> tiposBlocoObrigatorio = new List<string>
                {
                    TipoBlocoEnum.UH.ToDescription(),
                    TipoBlocoEnum.JUSMED.ToDescription(),
                    TipoBlocoEnum.CT.ToDescription(),
                    TipoBlocoEnum.TG.ToDescription(),
                    TipoBlocoEnum.BB.ToDescription()
                };

            bool isGerar = dados.Any();

            return isGerar;
        }

        private void GerarNomeArquivo()
        {
            if (DadosColetaBloco.Any(dado => dado.Insumo.TipoBloco == TipoBlocoEnum.TG.ToString()))
            {
                Nome = string.Format("GNL_{0:00}{1}_{2:yyyyMMddHHmm}.RV{3}",
                    SemanaOperativa.PMO.MesReferencia,
                    SemanaOperativa.PMO.AnoReferencia.ToString().Substring(2),
                    DateTime.Now,
                    SemanaOperativa.Revisao);
            }
            else
            {
                string nomeAgente = Agente.Nome;

                if (!string.IsNullOrEmpty(CodigoPerfilOns))
                {
                    ColetaInsumo coletaInsumo = DadosColetaBloco.First().ColetaInsumo;
                    nomeAgente = string.Format("{0}_{1}", Agente.Nome, coletaInsumo.CodigoPerfilONS);
                }

                Nome = string.Format("{0}_{1:00}{2}_{3:yyyyMMddHHmm}_{4:000}.RV{5}",
                    nomeAgente,
                    SemanaOperativa.PMO.MesReferencia,
                    SemanaOperativa.PMO.AnoReferencia.ToString().Substring(2),
                    DateTime.Now,
                    Agente.Id,
                    SemanaOperativa.Revisao);
            }
        }

    }
}