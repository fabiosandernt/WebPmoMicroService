using System;

namespace ONS.WEBPMO.Application.DTO
{
    public class InclusaoDadoColetaManutencaoDTO
    {
        public int IdColetaInsumo { get; set; }
        public string IdUsina { get; set; }
        public string IdUnidadeGeradora { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TempoRetorno { get; set; }
        public string Justificativa { get; set; }
        public string Numero { get; set; }
        public bool IsMonitorar { get; set; }
        public byte[] VersaoColetaInsumo { get; set; }

        public string Periodicidade { get; set; }
        public string Situacao { get; set; }
        public string ClassificacaoPorTipoEquipamento { get; set; }
        public bool EhDiaria { get {
                return this.Periodicidade == "D";
            }
        }
    }
}
