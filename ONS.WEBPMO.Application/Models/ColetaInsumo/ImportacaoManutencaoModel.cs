using System;

namespace ONS.WEBPMO.Application.Models.ColetaInsumo
{
    public class ImportacaoManutencaoModel
    {
        public int IdColetaInsumo { get; set; }
        public string IdUnidadeGeradora { get; set; }
        public string NomeUsina { get; set; }
        public string NomeUnidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TempoRetorno { get; set; }
        public string Justificativa { get; set; }
        public string Numero { get; set; }
        public string VersaoColetaInsumo { get; set; }

        public string Periodicidade { get; set; }
        public string Situacao { get; set; }
        public string ClassificacaoPorTipoEquipamento { get; set; }
    }
}