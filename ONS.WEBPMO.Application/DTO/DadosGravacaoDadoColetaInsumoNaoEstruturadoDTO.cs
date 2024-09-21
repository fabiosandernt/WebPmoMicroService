
namespace ONS.WEBPMO.Application.DTO
{
    public class DadosGravacaoDadoColetaInsumoNaoEstruturadoDTO
    {
        public int? IdDadoNaoEstruturado { get; set; }

        public int IdSemanaOperativa { get; set; }

        public int IdInsumo { get; set; }

        public int IdColetaInsumo { get; set; }

        public int IdAgente { get; set; }
        
        public string Observacao { get; set; }

        public ISet<ArquivoDadoNaoEstruturadoDTO> Arquivos { get; set; }

        public bool EnviarDadosAoSalvar { get; set; }

        public bool PreservarSituacaoDadoColeta { get; set; }

        public bool IsMonitorar { get; set; }

        public byte[] VersaoColetaInsumo { get; set; }
        
    }
}
